using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
public class Shanxiao_BossACT1 : Enemy
{
    public GameObject cage;
	public float minBoomerangTime, maxBoomerangTime;

	private MusicController music;
    private Player player1;
    private Player2 player2;
    private Rigidbody Mei;
    private Animator anim_cage;
    private GameObject tempCage;

	private bool runOnce = false;
	// private bool justSpawned = false;

	// Use this for initialization
	void Awake () {
        player1 = FindObjectOfType<Player>();
        player2 = FindObjectOfType<Player2>();
        Mei = GameObject.Find("Mei Lien").GetComponent<Rigidbody>();
		Invoke("SpawnCage", Random.Range(minBoomerangTime, maxBoomerangTime));
		music = FindObjectOfType<MusicController>();
		music.PlaySong(music.bossSong);
	}
	
	// void Update()
    // {
	// 	// Mei = GameObject.Find("Mei Lien")
	// 	Debug.Log("tempCage non-exist: " + (tempCage == null));
    //     // if(tempCage == null && tempCage.activeSelf == false) {
	// 	if(tempCage == null && justSpawned) {
	// 		Invoke("SpawnCage", Random.Range(minBoomerangTime, maxBoomerangTime));
	// 	}
    // }
	private void Update() {
		onGround = Physics.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
		anim.SetBool("Grounded", onGround);
		anim.SetBool("Dead", isDead);
		SpriteRenderer Sprite = gameObject.GetComponent<SpriteRenderer>();
		
		Debug.Log("isDead: "+ isDead);
		//FindCheckpoint.G
		if (!isDead)
		{
			facingRight = (realTarget.position.x < transform.position.x) ? false : true;
			Debug.Log("facingRight: "+ facingRight);
			if (facingRight)
			{
				transform.eulerAngles = new Vector3(0, 180, 0);
				// Sprite.flipX = true;
				//sprite.flipX;
				//var shadow = gameObject.transform.Find("shadow").gameObject;
				//shadow.transform.eulerAngles = new Vector3(0, 180, 0);
				//Debug.Log(shadow.transform.eulerAngles);

			}
			else
			{
				// Sprite.flipX = false;
				transform.eulerAngles = new Vector3(0, 0, 0);
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

		if(isDead && runOnce==false){
			runOnce = true;
			Debug.Log("Defeated");
			music.PlaySong(music.levelClearSong);
			FindObjectOfType<UIManager>().UpdateDisplayMessage("Level Clear");
			if(PhotonNetwork.IsMasterClient){
				Debug.Log("ran this");
				BossDefeated();
			}
		}
	}

	void SpawnCage()
	{
		if (!isDead)
		{
			// justSpawned = true;
			anim.SetTrigger("Boomerang");
			print("mei lien location: " + player2.transform.position);
			tempCage = PhotonNetwork.Instantiate(cage.gameObject.name,player2.transform.position, transform.rotation); //Instantiate(cage, player2.transform.position, transform.rotation);
			anim_cage = tempCage.GetComponent<Animator>();
            anim_cage.SetBool("Cage", true);
            Mei.constraints = RigidbodyConstraints.FreezeAll;
			Invoke("SpawnCage", Random.Range(minBoomerangTime, maxBoomerangTime));
		}
	}

	void BossDefeated()
	{
		Debug.Log("am ehre");
		PhotonNetwork.LoadLevel("VN_3");
		// FindObjectOfType<ResetCameraScript>().Activate();
		// Invoke("Playtemp", 8f);
		 //Invoke("LoadScene", 6f);
	}

	void LoadScene()
	{
				Debug.Log("am ehr2222e");
		PhotonNetwork.LoadLevel("Scroller_3_1");
		//SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	void Playtemp(){
		music.PlaySong(music.levelSong);
	}
}