using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class Player : MonoBehaviour
{

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
    public GameObject VNSayDialog;

    // Protected variables
    protected float currentSpeed;
    protected Animator anim;
    protected Transform groundCheck;
    protected GameObject playerAttack;
    protected Attack curAttack;
    protected bool onGround;
    protected bool isDead = false;
    protected bool isFacingRight = true;

    // Private variables
    private Vector3 dashVector;
    private bool isDash = false;
    private bool isAttack = false;
    private bool heavyAttack = false;
    private float dashTime;
    private float startDashTime = 0.1f;
    private float heavyAttackTime = 0f;
    private AudioSource audioS;
    private Vector2 inputVector;
    private Player2 player2;
    public PhotonView photonView;

    // Use this for initialization
    void Start()
    {
            photonView = GetComponent<PhotonView>();
            player2 = FindObjectOfType<Player2>();
            rb = GetComponent<Rigidbody>();
            anim = GetComponent<Animator>();
            groundCheck = gameObject.transform.Find("GroundCheck");
            currentSpeed = maxSpeed;
            currentHealth = maxHealth;
            audioS = GetComponent<AudioSource>();
            playerAttack = gameObject.transform.Find("Attack").gameObject;
            curAttack = playerAttack.GetComponent<Attack>();
            dashTime = startDashTime;
    }

    // Update is called once per frame
    void Update()
    {

            onGround = Physics.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
            anim.SetBool("OnGround", onGround);
        
        /*
        if (transform.position.y != 0)
        {
            Vector3 pos = transform.position;
            pos.y = 0.32f;
            transform.position = pos;
        }*/
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
                if (!isDash)
                {
                    if (h > 0 && !isFacingRight)
                    {
                         photonView.RPC("Flip", RpcTarget.All);//Flip();
                    }
                    else if (h < 0 && isFacingRight)
                    {
                        photonView.RPC("Flip", RpcTarget.All);//Flip();
                    }
                }

                // Debug.Log(isAttack);

                // Player dash functionality 
                if (!isDash && dashTime <= 0f)
                {
                    dashTime = startDashTime;
                    rb.velocity = Vector3.zero;
                }
                else if (!isAttack && isDash)
                {
                    dashTime -= Time.deltaTime;
                    rb.velocity = dashVector * dashForce;
                    StartCoroutine(setDashAnim());
                }

                IEnumerator setDashAnim()
                {
                    yield return new WaitForSeconds(0.5f);
                    anim.SetBool("IsDashing", false);
                    isDash = false;
                }

                // Player heavy attack cooldown counter
                if (heavyAttackTime <= 0f && !heavyAttack)
                {
                    heavyAttack = true;
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
            }
    }

    // Player 1's Attack Function
    [PunRPC]
    public void Attack()
    {
            if(photonView.IsMine && !isDash){
            Debug.Log("Player1 is doing an attack!");
            curAttack.damage = 1;
            isAttack = true;
            curAttack.attackState = "qinyangBasicAttack";
            anim.SetTrigger("Attack");
            //setAttack();
            Invoke("setAttack", 1);
            }
    }

    // Helper function for Player1's attack which reverses the isAttack value 
    // This prevents Qinyang's sprite from flipping mid animation
    private void setAttack()
    {

            if (isAttack)
            {
                isAttack = false;
            }
            else
            {
                isAttack = true;
            }
        
    }

    // Player 1's HeavyAttack Function
    public void HeavyAttack()
    {
        if(photonView.IsMine){
            if (heavyAttack)
            {
                Debug.Log("Player1 is doing a heavy attack!");
                anim.SetTrigger("HeavyAttack");
                curAttack.attackState = "qinyangHeavyAttack";
                curAttack.damage = 2;
                if (isFacingRight)
                {
                    curAttack.attackDirection = 1f;
                }
                else
                {
                    curAttack.attackDirection = -1f;
                }
                heavyAttackTime = 120f;
            }

            heavyAttack = false;
        }
    }


    // Player 1's Special Function
    public void Special()
    {
        
            Debug.Log("Player1 is doing a special attack!");
        
    }

    // Player 1's Dash Function
    public void Dash()
    {
        // If VN SayDialog is not active, then she can jump
        if (!(VNSayDialog.activeSelf) && !isAttack)
        {
            anim.SetBool("IsDashing", true);
            Debug.Log("Player1 is doing a dash!");
            isDash = true;
            if (isFacingRight)
            {
                dashVector = Vector3.right;
            }
            else
            {
                dashVector = Vector3.left;
            }
        }
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
[PunRPC]
    void Flip()
    {
        isFacingRight = !isFacingRight;
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
    [PunRPC]
    public void TookDamage(int damage)
    {
        if (!isDead)
        {
            currentHealth -= damage;
            anim.SetTrigger("HitDamage");

            PlaySound(damageSound, "Damage", damage);

            if (currentHealth <= 0)
            {
                playerDying();
                player2.playerDying();
            }
        }
    }

    // Helper function -> player dies and respawns if they reach 0 health
    public void playerDying()
    {
        isDead = true;
        anim.SetTrigger("Dead");
        FindObjectOfType<GameManager>().lives--;
        if (isFacingRight)
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

    public void PlayAnimSound(string path)
    {
        FMODUnity.RuntimeManager.PlayOneShot(path, GetComponent<Transform>().position);
    }

    private void OnTriggerStay(Collider other)
    {
        interactObj = other;
    }

    void PlayerRespawn()
    {
        if (FindObjectOfType<GameManager>().lives > 0)
        {
            isDead = false;
            FindObjectOfType<UIManager>().UpdateLives();
            currentHealth = maxHealth;
            FindObjectOfType<UIManager>().UpdateHealth();
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public int curHealth()
    {
        return currentHealth;
    }
}
