using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Player2 : MonoBehaviour
{
    // Player attributes
    // Public variables 
    public int playerIndex;
    public float maxSpeed;
    public float jumpForce;
    public float minHeight, maxHeight;
    // public int maxHealth;
    public Collider interactObj;
    public AudioClip punchSound, collisionSound, healthItem;
    public string jumpSound, damageSound;

    // Private variables
    private bool onGround2;
    private bool isDead2 = false;
    private bool isFacingRight2 = false;
    private bool canAttack = false;
    private float currentSpeed;
    private float torchDistance;
    private float attackTime = 0f;

    // GameObjects
    private Player player1;
    private Rigidbody rb;
    private GameObject torch;
    private Animator anim2;
    private Transform groundCheck2;
    private Vector2 inputVector;
    private TorchControllerSS torchControl;
    public AudioSource currAudioSource;
    public GameObject projectile;
    public PhotonView photonView;

    public AudioClip collisionSound2, jumpSound2, healthItem2;

    // Initialization
    void Awake()
    {
        photonView = GetComponent<PhotonView>();
        player1 = FindObjectOfType<Player>();
        rb = GetComponent<Rigidbody>();
        anim2 = GetComponent<Animator>();
        torch = GameObject.FindGameObjectWithTag("Torch");
        currAudioSource = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        groundCheck2 = gameObject.transform.Find("GroundCheck2");
        currentSpeed = maxSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        // Set onGround and animation bools
        onGround2 = Physics.Linecast(transform.position, groundCheck2.position, 1 << LayerMask.NameToLayer("Ground"));
        anim2.SetBool("OnGround", onGround2);
        anim2.SetBool("Dead", isDead2);
    }

    public void SetInputVector(Vector2 direction)
	{
		inputVector = direction;
	}

    // Updates independently of frame
    private void FixedUpdate()
    {
        if (!isDead2)
        {
            // Player 2 Movement
            if (onGround2) anim2.SetFloat("Speed", 0.1f + Mathf.Abs(rb.velocity.magnitude));
            float h = inputVector.x;
            float v = inputVector.y;

            // Debug.Log(onGround2);

            if (!onGround2)
            {
                v = 0;
                currentSpeed = (maxSpeed * 1.5f);
            }
            else
            {
                currentSpeed = maxSpeed;
            }

            rb.velocity = new Vector3(h * currentSpeed, rb.velocity.y, v * currentSpeed);

            // Flips sprite based on movement
            if(h < 0 && !isFacingRight2)
            {
                 photonView.RPC("Flip", RpcTarget.All);//Flip();
            } else if(h > 0 && isFacingRight2)
            {
                 photonView.RPC("Flip", RpcTarget.All);//Flip();
            }

            // Attack cooldown
            if(attackTime <= 0 && !canAttack)
            {
                canAttack = true;
            } else
            {
                attackTime -= 1f;
            }

            float minWidth = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 10)).x;
			float maxWidth = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 10)).x;
			rb.position = new Vector3(Mathf.Clamp(rb.position.x, minWidth + 1, maxWidth - 1),
				rb.position.y,
				Mathf.Clamp(rb.position.z, minHeight, maxHeight));

            // Torch Interaction
            Vector3 torchPosition = torch.transform.position - transform.position;
            torchDistance = torchPosition.x;
            torchControl = torch.GetComponent<TorchControllerSS>();
        }
    }

    // Player 2's Attack Function
    [PunRPC] 
    public void Attack()
    {
        if (onGround2 && canAttack)
        {
            Debug.Log("Player2 is doing an attack!");
            anim2.SetTrigger("Attack");
            // Spawn projectile and get properties
            //Vector3 projectileScale;
            //Vector3 tempPosition = transform.position;
            Vector3 tempPosition;
            if (!isFacingRight2)
            {
                tempPosition = new Vector3(transform.position.x + 2,
                transform.position.y,
                Mathf.Clamp(transform.position.z, minHeight, maxHeight));
            }
            else
            {
                tempPosition = new Vector3(transform.position.x - 2,
                transform.position.y,
                Mathf.Clamp(transform.position.z, minHeight, maxHeight));
            }


            GameObject newProjectile = Instantiate(projectile, tempPosition, Quaternion.identity) as GameObject;//PhotonNetwork.Instantiate("Projectile", tempPosition, Quaternion.identity) as GameObject;
            //Instantiate(projectile, tempPosition, Quaternion.identity) as GameObject;
            if (isFacingRight2)
            {
                Debug.Log("Does this run in p1");
                Vector3 projectileScale = newProjectile.transform.localScale;
                projectileScale.x *= -1;
                newProjectile.transform.localScale = projectileScale;
            }
            Projectile nProj = newProjectile.GetComponent<Projectile>();
            StartCoroutine(MoveWaterwave(newProjectile, nProj));
            

            attackTime = 100f;
            canAttack = false;
        }
    }

    IEnumerator MoveWaterwave(GameObject newProjectile, Projectile nProj) {
        yield return new WaitForSeconds(0.5f);
        // Flip direction and sprite orientation depending on where Mei Lien is facing
        if (!isFacingRight2)
        {
            newProjectile.GetComponent<Rigidbody>().AddForce(200f, 0, 0);
            nProj.attackDir = 1f;
        }
        else
        {
            nProj.attackDir = -1f;
            newProjectile.GetComponent<Rigidbody>().AddForce(-200f, 0, 0);
        }
    }

    // Player 1's HeavyAttack Function
    public void HeavyAttack()
    {
        Debug.Log("Player2 is doing a heavy attack!");
    }

    // Player 2's Special Function
    public void Special()
    {
        Debug.Log("Player2 is doing a special!");
    }

    // Player 2's Jump Function
    public void Jump()
    {
        if (onGround2)
        {
            Debug.Log("Player2 is doing a jump!");
            anim2.SetTrigger("Jumping");
            rb.AddForce(Vector3.up * jumpForce);
        }
    }

    // Player 2's Interact Function
    public void Interact(Collider other)
    {
        if (other.CompareTag("Health Item"))
        {
            Destroy(other.gameObject);
            interactObj = null;
            anim2.SetTrigger("Catching");
            PlaySong(healthItem);
            player1.currentHealth = player1.maxHealth;
        }

        if (other.CompareTag("Torch"))
            print("here");
        {
            if (torchDistance <= 1.5f && !torchControl.isLit)
            {
                print("Lighting lantern!");
                torchControl.lightLantern();
            }
            else if (torchDistance <= 1.5f && torchControl.isLit)
            {
                torchControl.darkLantern();
            }
        }
    }

    // Flip function for flipping sprite when facing in a direction
    [PunRPC]
    private void Flip()
    {
        isFacingRight2 = !isFacingRight2;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    // Hit Damage function
    public void TookDamage(int damage) 
    {
        if (!isDead2 && onGround2)
        {
            player1.currentHealth-= damage;
            anim2.SetTrigger("HitDamage");

            PlaySound(damageSound, "Damage", damage);

            if (player1.currentHealth <= 0)
            {
                playerDying();
                player1.playerDying();
            }
        }
    }

    // Helper function -> player dies and respawns if they reach 0 health
    public void playerDying()
    {
        isDead2 = true;
        //FindObjectOfType<GameManager>().lives--;
        if (isFacingRight2)
        {
            rb.AddForce(new Vector3(-3, 5, 0), ForceMode.Impulse);
        }
        else
        {
            rb.AddForce(new Vector3(3, 5, 0), ForceMode.Impulse);
        }
        Invoke("PlayerRespawn", 2f);
    }

    public void PlaySong(AudioClip clip)
    {
        currAudioSource.clip = clip;
        currAudioSource.Play();
    }

	public void PlaySound(string path, string parameterName, int parameterValue)
	{
		FMOD.Studio.EventInstance sfx = FMODUnity.RuntimeManager.CreateInstance(path);
		sfx.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(GetComponent<Transform>().position));
		sfx.setParameterByName(parameterName, parameterValue);
		sfx.start();
		sfx.release();
	}

	public void PlayAnimSound(string path)
	{
		FMODUnity.RuntimeManager.PlayOneShot(path, GetComponent<Transform>().position);
	}
    
    private void OnTriggerStay(Collider other)
    {
        interactObj = other;
    }

    // Player Respawn function
    void PlayerRespawn()
    {
        if (FindObjectOfType<GameManager>().lives > 0)
        {
            isDead2 = false;
            FindObjectOfType<UIManager>().UpdateLives();
            player1.currentHealth = player1.maxHealth;
            FindObjectOfType<UIManager>().UpdateHealth();
            anim2.Rebind();
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
}
