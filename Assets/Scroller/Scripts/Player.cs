using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

	public int playerIndex = 0;
	public float maxSpeed = 4;
	public float jumpForce = 400;
	public float minHeight, maxHeight;
	public int maxHealth = 10;
	public string playerName;
	public Sprite playerImage;
	public AudioClip punchSound, collisionSound, jumpSound, healthItem;

	public static int currentHealth;
	public Collider interactObj;
	protected float currentSpeed;
	public Rigidbody rb;
	protected Animator anim;
	protected Transform groundCheck;
	protected bool onGround;
	protected bool isDead = false;
	protected bool facingRight = true;
	private bool isjump = false;
	private AudioSource audioS;
	private Vector2 inputVector;
	// QinyangControls controls;
	// Vector2 move;


	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		anim = GetComponent<Animator>();
		groundCheck = gameObject.transform.Find("GroundCheck");
		currentSpeed = maxSpeed;
		currentHealth = maxHealth;
		audioS = GetComponent<AudioSource>();

	}
	
	// Update is called once per frame
	void Update () {
		
		
		// print(currentHealth);
		onGround = Physics.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
		anim.SetBool("OnGround", onGround);
		anim.SetBool("Dead", isDead);

		// if (Input.GetButtonDown("Fire1"))
		// {
		// 	anim.SetTrigger("Attack");
		// 	PlaySong(collisionSound);
		// }

		// print(currentHealth);
	}

	public void SetInputVector(Vector2 direction)
	{
		inputVector = direction;
	}

	private void FixedUpdate()
	{
		if (!isDead)
		{
			// float h = Input.GetAxis("Horizontal");
			// float v = Input.GetAxis("Vertical");
			float h = inputVector.x;
			float v = inputVector.y;
			// Vector3 m = new Vector3(move.x * currentSpeed, rb.velocity.y,  * currentSpeed) * Time.deltaTime;
        // transform.Translate(m, Space.World);
			if (!onGround)
				v = 0;

			rb.velocity = new Vector3(h * currentSpeed, rb.velocity.y, v * currentSpeed);

			if (onGround)
				anim.SetFloat("Speed", Mathf.Abs(rb.velocity.magnitude));

			if(h > 0 && !facingRight)
			{
				Flip();
			}
			else if(h < 0 && facingRight)
			{
				Flip();
			}

			if (onGround && isjump)
			{
				isjump = false;
				rb.AddForce(Vector3.up * jumpForce);
				PlaySong(jumpSound);
			}
			float minWidth = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 10)).x;
			float maxWidth = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 10)).x;
			rb.position = new Vector3(Mathf.Clamp(rb.position.x, minWidth + 1, maxWidth - 1),
				rb.position.y,
				Mathf.Clamp(rb.position.z, minHeight, maxHeight));
		}
	}

	public void Attack()
    {
        anim.SetTrigger("Attack");
		PlaySong(collisionSound);
    }

	public void Jump()
    {
        isjump = true;
    }

	public void Interact(Collider other)
	{
		if (other.CompareTag("Health Item"))
		{
			Destroy(other.gameObject);
			interactObj = null;
			anim.SetTrigger("Catching");
			PlaySong(healthItem);
			currentHealth = maxHealth;
			// FindObjectOfType<UIManager>().UpdateHealth(currentHealth);
		}
        
    }

	void Flip()
	{
		facingRight = !facingRight;

		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}

	void ZeroSpeed()
	{
		currentSpeed = 0;
	}

	void ResetSpeed()
	{
		currentSpeed = maxSpeed;
	}

	public void TookDamage(int damage)
	{
		if (!isDead)
		{
			currentHealth -= damage;
			anim.SetTrigger("HitDamage");
			// FindObjectOfType<UIManager>().UpdateHealth(currentHealth);
			PlaySong(collisionSound);
			if(currentHealth <= 0)
			{
				isDead = true;
				FindObjectOfType<GameManager>().lives--;
				
				if (facingRight)
				{
					rb.AddForce(new Vector3(-3, 5, 0), ForceMode.Impulse);
				}
				else
				{
					rb.AddForce(new Vector3(3, 5, 0), ForceMode.Impulse);
				}
			}
		}
	}

	public void PlaySong(AudioClip clip)
	{
		audioS.clip = clip;
		audioS.Play();
	}

	private void OnTriggerStay(Collider other)
	{
		interactObj = other;
	}

	void PlayerRespawn()
	{
		if(FindObjectOfType<GameManager>().lives > 0)
		{
			isDead = false;
			FindObjectOfType<UIManager>().UpdateLives();
			currentHealth = maxHealth;
			// FindObjectOfType<UIManager>().UpdateHealth(currentHealth);
			anim.Rebind();
			float minWidth = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 10)).x;
			transform.position = new Vector3(minWidth, 10, -4);
		}
		else
		{
			FindObjectOfType<UIManager>().UpdateDisplayMessage("Game Over");
			Destroy(FindObjectOfType<GameManager>().gameObject);
			Invoke("LoadScene", 2f);
		}
		
	}

	void LoadScene()
	{
		SceneManager.LoadScene(0);
	}


	public int curHealth() {
		return currentHealth;}
}
