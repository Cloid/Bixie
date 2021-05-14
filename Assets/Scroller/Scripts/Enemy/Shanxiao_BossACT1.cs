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
		if(PhotonNetwork.IsMasterClient && isDead && runOnce==false){
			runOnce = true;
			Debug.Log("Defeated");
			music.PlaySong(music.levelClearSong);
			FindObjectOfType<UIManager>().UpdateDisplayMessage("Level Clear");
			BossDefeated();
		}
	}

	void SpawnCage()
	{
		if (!isDead)
		{
			// justSpawned = true;
			// anim_cage.SetTrigger("cage");
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
		// FindObjectOfType<ResetCameraScript>().Activate();
		// Invoke("Playtemp", 8f);
		 Invoke("LoadScene", 6f);
	}

	void LoadScene()
	{
		PhotonNetwork.LoadLevel("Scroller_3_1");
		//SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	void Playtemp(){
		music.PlaySong(music.levelSong);
	}
}