using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

	// Player attributes
	// Public variables
	public int playerIndex;
	public float maxSpeed;
	public float dashForce;
	public float minHeight, maxHeight;
	public int maxHealth;
	public string playerName;
	public Sprite playerImage;
	public Rigidbody rb;
	public AudioClip punchSound, collisionSound, jumpSound, healthItem;
	public string hitSound, damageSound;
	public int currentHealth;
	public Collider interactObj;

	// Protected variables
	protected float currentSpeed;
	protected Animator anim;
	protected Transform groundCheck;
	protected bool onGround;
	protected bool isDead = false;
	protected bool facingRight = true;

	// Private variables
	private bool isDash = false;
	private float dashTime = 0f;
	private AudioSource audioS;
	private Vector2 inputVector;

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
		onGround = Physics.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
		anim.SetBool("OnGround", onGround);
		anim.SetBool("Dead", isDead);
	}

	public void SetInputVector(Vector2 direction)
	{
		inputVector = direction;
	}

	private void FixedUpdate()
	{
		if (!isDead)
		{
			// Player 1 Movement
			float h = inputVector.x;
			float v = inputVector.y;
			if (!onGround)
				v = 0;

			rb.velocity = new Vector3(h * currentSpeed, rb.velocity.y, v * currentSpeed);

			if (onGround)
				anim.SetFloat("Speed", Mathf.Abs(rb.velocity.magnitude));

			// Flips sprite based on movement
			if (h > 0 && !facingRight)
			{
				Flip();
			}
			else if(h < 0 && facingRight)
			{
				Flip();
			}

            if (isDash && dashTime == 0f)
            {
				rb.velocity = rb.velocity * dashForce;
				PlaySong(jumpSound);
				isDash = false;
				dashTime = 100f;
			} else {
				if(dashTime > 0f)
                {
					dashTime -= 1f;
				}
            }

			float minWidth = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 10)).x;
			float maxWidth = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 10)).x;
			rb.position = new Vector3(Mathf.Clamp(rb.position.x, minWidth + 1, maxWidth - 1),
				rb.position.y,
				Mathf.Clamp(rb.position.z, minHeight, maxHeight));
		}
	}

	// Player 1's Attack Function
	public void Attack()
    {
		Debug.Log("Player1 is doing an attack!");
		anim.SetTrigger("Attack");

		//PlaySong(collisionSound);
		PlaySound(hitSound, "Hit Damage", 40);
    }

	// Player 1's HeavyAttack Function
	public void HeavyAttack()
	{
		Debug.Log("Player1 is doing a heavy attack!");
	}


	// Player 1's Special Function
	public void Special()
	{
		Debug.Log("Player1 is doing a special attack!");
	}

	// Player 1's Dash Function
	public void Dash()
    {
		Debug.Log("Player1 is doing a dash!");
		// If player is jumping, add vertical force
		isDash = true;
	}

	// Player 1's Interact Function
	public void Interact(Collider other)
	{
		if (other.CompareTag("Health Item"))
		{
			Destroy(other.gameObject);
			interactObj = null;
			anim.SetTrigger("Catching");
			PlaySong(healthItem);
			currentHealth = maxHealth;
		}
        
    }

	// Player 1's flip function
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

	// Hit Damage function
	public void TookDamage(int damage)
	{
		if (!isDead)
		{
			currentHealth -= damage;
			anim.SetTrigger("HitDamage");

			//PlaySong(collisionSound);
			PlaySound(damageSound, "Damage", damage);

			Debug.Log(currentHealth);
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

	public void PlaySound(string path, string parameterName, int parameterValue)
	{
		FMOD.Studio.EventInstance sfx = FMODUnity.RuntimeManager.CreateInstance(path);
		sfx.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(GetComponent<Transform>().position));
		sfx.setParameterByName(parameterName, parameterValue);
		sfx.start();
		sfx.release();
	}

	public void PlayFootstepsSound(string path)
	{
		FMODUnity.RuntimeManager.PlayOneShot(path, GetComponent<Transform>().position);
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

	public int curHealth()
	{
		return currentHealth;
	}
}
