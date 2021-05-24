﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;
using Fungus;

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
    public bool isHit = false;
    public Flowchart VN_Controller;

    // Private variables
    private bool onGround2;
    private bool isDead2 = false;
    private bool isFacingRight2 = false;
    private bool canAttack = false;
    private bool canHeavyAttack = false;
    private float currentSpeed;
    private float torchDistance;
    private float attackTime = 0f;
    private float heavyAttackTime = 0f;
    private QinyangControls controls;

    // GameObjects
    private Player player1;
    private Rigidbody rb;
    private GameObject torch;
    private Animator anim2;
    private Transform groundCheck2;
    private Vector2 inputVector;
    private TorchControllerSS torchControl;
    private List<GameObject> healthItems;
    public AudioSource currAudioSource;
    public Projectile projectile;
    public PhotonView photonView;
    public GameObject VNSayDialog;

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

        // controls = new QinyangControls();
        // controls.Gameplay.Interact.performed += 
    }
    // Start is called before the first frame update
    void Start()
    {
        groundCheck2 = gameObject.transform.Find("GroundCheck2");
        currentSpeed = maxSpeed;
        healthItems = new List<GameObject>();
        healthItems.AddRange(GameObject.FindGameObjectsWithTag("Health Item"));
    }

    // Update is called once per frame
    void Update()
    {
        // Set onGround and animation bools
        onGround2 = Physics.Linecast(transform.position, groundCheck2.position, 1 << LayerMask.NameToLayer("Ground"));
        anim2.SetBool("OnGround", onGround2);
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

            // Heavy Attack Cooldown
            if (heavyAttackTime <= 0 && !canHeavyAttack)
            {
                canHeavyAttack = true;
            }
            else
            {
                heavyAttackTime -= 1f;
            }

            float minWidth = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 10)).x;
			float maxWidth = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 10)).x;
			rb.position = new Vector3(Mathf.Clamp(rb.position.x, minWidth + 1, maxWidth - 1),
				rb.position.y,
				Mathf.Clamp(rb.position.z, minHeight, maxHeight));

            // Torch Interaction
            // Vector3 torchPosition = torch.transform.position - transform.position;
            // torchDistance = torchPosition.x;
            // torchControl = torch.GetComponent<TorchControllerSS>();
        }
    }

    // Player 2's Attack Function - Knockback Wave
    [PunRPC] 
    public void Attack()
    {
        if (onGround2 && canAttack)
        {
            Debug.Log("Player2 is doing an attack!");
            anim2.SetTrigger("Attack");
            // Spawn projectile and get properties
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

            Projectile newProjectile = Instantiate(projectile, tempPosition, Quaternion.identity) as Projectile;
            newProjectile.GetComponent<Projectile>().projSprite("WaterWave");
            if (isFacingRight2)
            {
                //Debug.Log("Does this run in p2");
                Vector3 projectileScale = newProjectile.transform.localScale;
                projectileScale.x *= -1;
                newProjectile.transform.localScale = projectileScale;
            }
            StartCoroutine(MoveProjectile(newProjectile));
            
            // Spawn FMOD attack sound **maybe attach to the wave for better effect
            FMODUnity.RuntimeManager.PlayOneShot("event:/Sounds/M_Attack", newProjectile.GetComponent<Transform>().position);

            attackTime = 100f;
            canAttack = false;
        }
    }

    // Player 2's HeavyAttack Function - Ice Freeze Attack
    [PunRPC]
    public void HeavyAttack()
    {
        if (heavyAttackTime <= 0 && canAttack)
        {
            Debug.Log("Player2 is doing a heavy attack!");
            anim2.SetTrigger("Attack");
            // Spawn projectile and get properties
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

            Projectile newProjectile = Instantiate(projectile, tempPosition, Quaternion.identity) as Projectile;
            newProjectile.GetComponent<Projectile>().projSprite("IceBall");
            if (isFacingRight2)
            {
                //Debug.Log("Does this run in p2");
                Vector3 projectileScale = newProjectile.transform.localScale;
                projectileScale.x *= -1;
                newProjectile.transform.localScale = projectileScale;
            }
            StartCoroutine(MoveProjectile(newProjectile));

            // Spawn FMOD attack sound **maybe attach to the wave for better effect
            FMODUnity.RuntimeManager.PlayOneShot("event:/Sounds/M_Attack", newProjectile.GetComponent<Transform>().position);

            heavyAttackTime = 180f;
            canHeavyAttack = false;
        }
    }

    IEnumerator MoveProjectile(Projectile newProjectile) {
        yield return new WaitForSeconds(0f);
        // Flip direction and sprite orientation depending on where Mei Lien is facing
        if (!isFacingRight2)
        {
            newProjectile.GetComponent<Rigidbody>().AddForce(200f, 0, 0);
            newProjectile.attackDir = 1f;
        }
        else
        {

            newProjectile.attackDir = -1f;
            newProjectile.GetComponent<Rigidbody>().AddForce(-200f, 0, 0);
        }
    }

    // Player 2's Special Function
    public void Special()
    {
        Debug.Log("Player2 is doing a special!");
    }

    // Player 2's Jump Function
    public void Jump()
    {
        // If player is on ground and VN SayDialog is not active, then she can jump
        if (onGround2 && !(VNSayDialog.activeSelf))
        {
            Debug.Log("Player2 is doing a jump!");
            anim2.SetTrigger("Jumping");
            if(transform.position.y > 2){
                rb.AddForce(Vector3.up * jumpForce/2);
            } else {
                rb.AddForce(Vector3.up * jumpForce);
            }
        }
    }

    // Player 2 collision function
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Health Item"))
        {
            healthItems.Remove(other.gameObject);
            Destroy(other.gameObject);
            //anim.SetTrigger("Catching");
            //PlaySong(healthItem);
            player1.currentHealth = player1.maxHealth;
        }
    }

    // Player 2's Interact Function
    // public void Interact(Collider other)
    // {
    //     if (other.CompareTag("Health Item"))
    //     {
    //         Destroy(other.gameObject);
    //         interactObj = null;
    //         anim2.SetTrigger("Catching");
    //         PlaySong(healthItem);
    //         player1.currentHealth = player1.maxHealth;
    //     }



    //     if (other.CompareTag("Torch"))
    //     {
    //         if (torchDistance <= 1.5f && !torchControl.isLit)
    //         {
    //             print("Lighting lantern!");
    //             torchControl.lightLantern();
    //         }
    //         else if (torchDistance <= 1.5f && torchControl.isLit)
    //         {
    //             torchControl.darkLantern();
    //         }
    //     }
    // }

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
    [PunRPC]
    public void TookDamage(float damage) 
    {
        if (!isDead2 && onGround2)
        {
            if(VN_Controller!=null){
                VN_Controller.ExecuteBlock("ML_Hit");
            }
            
            isHit = true;
            player1.currentHealth-= damage;
            anim2.SetTrigger("HitDamage");

            PlaySound(damageSound, "Damage", (int)damage);
            StartCoroutine(setHit());

            if (player1.currentHealth <= 0)
            {
                player1.playerDying();
                playerDying();
            }
        }
    }

    IEnumerator setHit()
    {
        yield return new WaitForSeconds(0.5f);
        isHit = false;
    }

    // Helper function -> player dies and respawns if they reach 0 health
    public void playerDying()
    {
        isDead2 = true;
        anim2.SetTrigger("Dead");
        //FindObjectOfType<GameManager>().lives--;
        if (isFacingRight2)
        {
            rb.AddForce(new Vector3(-3, 5, 0), ForceMode.Impulse);
        }
        else
        {
            rb.AddForce(new Vector3(3, 5, 0), ForceMode.Impulse);
        }
        //Invoke("PlayerRespawn", 2f);
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
        /*
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
        }*/
    }
}
