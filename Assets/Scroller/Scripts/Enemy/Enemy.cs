using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Photon.Pun;

public class Enemy : MonoBehaviour {

	public float maxSpeed;
	public float minHeight, maxHeight;
	public float damageTime = 0.5f;
	public int maxHealth;
	public float attackRate = 1f;
	public string enemyName;
	
	public Sprite enemyImage;
	public AudioClip collisionSound;
	public string damageSound, deathSound;
	public PhotonView photonView;

	private int currentHealth;
	private float currentSpeed;
	private Rigidbody rb;
	protected Animator anim;
	private Transform groundCheck;
	private bool onGround;
	protected bool facingRight = false;
	private Transform target;
	private Transform target2;
	protected bool isDead = false;
	private float zForce;
	private float walkTimer;
	private bool damaged = false;
	private float damageTimer;
	private float nextAttack;
	private AudioSource audioS;

	//global event handler for enemies
	public delegate void UnitEventHandler(GameObject Unit);
	//global event Handler for destroying units
	public static event UnitEventHandler OnUnitDestroy;

	//[Header("Attack Data")]
	//public DamageObject[] AttackList; //a list of attacks
	//public bool PickRandomAttack; //choose a random attack from the list
	//public float hitZRange = 2; //the z range of all attacks
	//public float defendChance = 0; //the chance that an incoming attack is defended %
	//public float hitRecoveryTime = .4f; //the timeout after a hit before the enemy can do an action
	//public float standUpTime = 1.1f; //the time it takes for this enemy to stand up
	//public bool canDefendDuringAttack; //true if the enemy is able to defend an incoming attack while he is doing his own attack
	//public bool AttackPlayerAirborne; //attack a player while he is in the air
	//private DamageObject lastAttack; //data from the last attack that has taken place
	//private int AttackCounter = 0; //current attack number
	//public bool canHitEnemies; //true is this enemy can hit other enemies
	//public bool canHitDestroyableObjects; //true is this enemy can hit destroyable objects like crates, barrels.
	//[HideInInspector]
	//public float lastAttackTime; //time of the last attack

	public enum ENEMYTACTIC
	{
		ENGAGE = 0,
		KEEPCLOSEDISTANCE = 1,
		KEEPMEDIUMDISTANCE = 2,
		KEEPFARDISTANCE = 3,
		STANDSTILL = 4,
	}

	public enum RANGE
	{
		ATTACKRANGE,
		CLOSERANGE,
		MIDRANGE,
		FARRANGE,
	}

	// Use this for initialization
	void Start () {
		photonView = GetComponent<PhotonView>();
		rb = GetComponent<Rigidbody>();
		anim = GetComponent<Animator>();
		groundCheck = transform.Find("GroundCheck");
		//target = GameObject.FindGameObjectWithTag("Player").transform;
		// target = FindObjectOfType<Player>().transform;
		//target2 = FindObjectOfType<Player2>().transform;
		//print(GameObject.FindGameObjectWithTag("Player").transform);
		//print(GameObject.FindGameObjectWithTag("Player2").transform);
		currentHealth = maxHealth;
		audioS = GetComponent<AudioSource>();
		Debug.Log("Current Health: " + currentHealth);
	}
	
	// Update is called once per frame
	void Update () {
		if(target2 == null){
			target2 = FindObjectOfType<Player2>().transform; 
		}

		onGround = Physics.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
		anim.SetBool("Grounded", onGround);
		anim.SetBool("Dead", isDead);
		SpriteRenderer Sprite = gameObject.GetComponent<SpriteRenderer>();
		//FindCheckpoint.G
		if (!isDead)
		{
			facingRight = (target2.position.x < transform.position.x) ? false : true;
			Debug.Log("facingRight: "+ facingRight);
			if (facingRight)
			{
				//transform.eulerAngles = new Vector3(0, 180, 0);
				Sprite.flipX = true;
				//sprite.flipX;
				//var shadow = gameObject.transform.Find("shadow").gameObject;
				//shadow.transform.eulerAngles = new Vector3(0, 180, 0);
				//Debug.Log(shadow.transform.eulerAngles);

			}
			else
			{
				Sprite.flipX = false;
				//transform.eulerAngles = new Vector3(0, 0, 0);
				//var shadow = gameObject.transform.Find("shadow").gameObject;
				//shadow.transform.eulerAngles = new Vector3(0, 0, 0);
			}
		}
		

		if(damaged && !isDead)
		{
			damageTimer += Time.deltaTime;
			if(damageTimer >= damageTime)
			{
				damaged = false;
				damageTimer = 0;
			}
		}

		walkTimer += Time.deltaTime;
	}

	private void FixedUpdate()
	{
		// if (!isDead)
		// {
		// 	// Finds distance between:
		// 	// targetDistance = player 1 (Qinyang)
		// 	// targetDistance = player 2 (Mei Lien)
		// 	Vector3 targetDistance = target.position - transform.position;
		// 	Vector3 targetDistance2 = target2.position - transform.position;
		// 	float hForce;
		// 	if (targetDistance.x < targetDistance2.x)
		// 	{
		// 		 hForce = targetDistance.x / Mathf.Abs(targetDistance.x);
		// 	} else
        //     {
		// 		hForce = targetDistance2.x / Mathf.Abs(targetDistance2.x);
        //     }

		// 	if(walkTimer >= Random.Range(1f, 2f))
		// 	{
		// 		zForce = Random.Range(-1, 2);
		// 		walkTimer = 0;
		// 	}

		// 	if(Mathf.Abs(targetDistance.x) < 1.5f || Mathf.Abs(targetDistance2.x) < 1.5f)
		// 	{
		// 		hForce = 0;
		// 	}

		// 	if(!damaged)
		// 	rb.velocity = new Vector3(hForce * currentSpeed, 0, zForce * currentSpeed);

		// 	anim.SetFloat("Speed", Mathf.Abs(currentSpeed));

		// 	if((Mathf.Abs(targetDistance.x) < 1.5f && Mathf.Abs(targetDistance.z) < 1.5f && Time.time > nextAttack) ||
		// 		(Mathf.Abs(targetDistance2.x) < 1.5f && Mathf.Abs(targetDistance2.z) < 1.5f && Time.time > nextAttack))
		// 	{
		// 		anim.SetTrigger("Attack");
		// 		currentSpeed = 0;
		// 		nextAttack = Time.time + attackRate;
		// 	}
		// }

		if (!isDead)
		{
			Vector3 targetDitance = target2.position - transform.position;
			float hForce = targetDitance.x / Mathf.Abs(targetDitance.x);

			if(walkTimer >= Random.Range(1f, 2f))
			{
				// zForce = Random.Range(-9, 3);
				zForce = targetDitance.z / Mathf.Abs(targetDitance.z);
				walkTimer = 0;
			}

			if(Mathf.Abs(targetDitance.x) < 1.5f)
			{
				hForce = 0;
			}

			if(!damaged)
			rb.velocity = new Vector3(hForce * currentSpeed, 0, zForce * currentSpeed);

			anim.SetFloat("Speed", Mathf.Abs(currentSpeed));

			if(Mathf.Abs(targetDitance.x) < 1.5f && Mathf.Abs(targetDitance.z) < 1.5f && Time.time > nextAttack)
			{
				anim.SetTrigger("Attack");
				currentSpeed = 0;
				nextAttack = Time.time + attackRate;
			}
		}

		rb.position = new Vector3
			(
				rb.position.x,
				rb.position.y,
				Mathf.Clamp(rb.position.z, minHeight, maxHeight));
	}

	[PunRPC]
	public void TookDamage(int damage, string stateTag, float attackDir)
	{
		Debug.Log("Current Health: " + currentHealth);
		Debug.Log("State Tag: "+ stateTag);
		if (!isDead)
		{
			damaged = true;
			currentHealth -= damage;
			anim.SetTrigger("HitDamage");
			PlaySound(damageSound, "Damage", damage);
			FindObjectOfType<UIManager>().UpdateEnemyUI(maxHealth, currentHealth, enemyName, enemyImage);
			// Enemies get an effect depending on stateTag
			switch(stateTag)
            {
				case "qinyangBasicAttack":
					break;
				case "qinyangHeavyAttack":
					rb.AddForce(new Vector3(attackDir * 10, 0, 0), ForceMode.Impulse);
					break;
				case "meiLienBasicAttack":
					rb.AddForce(new Vector3(attackDir * 10, 0, 0), ForceMode.Impulse);
					break;
				case "meiLienHeavyAttack":
					break;
				default:
					Debug.Log("Unregistered enemy state tag");
					break;
			}
			if(currentHealth <= 0)
			{
				isDead = true;
				rb.AddRelativeForce(new Vector3(3, 5, 0), ForceMode.Impulse);
				PlaySound(deathSound, "Damage", damage);
				DisableEnemy();
				//Destroy(gameObject);
				photonView.RPC("DestroyEnemy", RpcTarget.AllBuffered);
			}
		}
	}

	[PunRPC]
	public void DestroyEnemy(){
		Destroy(gameObject);
	}
	public void DisableEnemy()
	{
		gameObject.SetActive(false);
	}

	void ResetSpeed()
	{
		currentSpeed = maxSpeed;
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

	public void DestroyUnit()
	{
		if (OnUnitDestroy != null) OnUnitDestroy(gameObject);
	}
}
