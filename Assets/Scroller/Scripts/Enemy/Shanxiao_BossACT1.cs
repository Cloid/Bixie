using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

	// Use this for initialization
	void Awake () {
        player1 = FindObjectOfType<Player>();
        player2 = FindObjectOfType<Player2>();
        Mei = GameObject.Find("Mei Lien").GetComponent<Rigidbody>();
		// Invoke("SpawnCage", Random.Range(minBoomerangTime, maxBoomerangTime));
		music = FindObjectOfType<MusicController>();
		music.PlaySong(music.bossSong);
	}
	
	void SpawnCage()
	{
		if (!isDead)
		{
			// anim_cage.SetTrigger("cage");
			GameObject tempCage = Instantiate(cage, player2.transform.position, transform.rotation);
			anim_cage = tempCage.GetComponent<Animator>();
            anim_cage.SetBool("Cage", true);
            Mei.constraints = RigidbodyConstraints.FreezeAll;
		}
	}

	void BossDefeated()
	{
		music.PlaySong(music.levelClearSong);
		FindObjectOfType<UIManager>().UpdateDisplayMessage("Level Clear");
		// FindObjectOfType<ResetCameraScript>().Activate();
		// Invoke("Playtemp", 8f);
		// Invoke("LoadScene", 8f);
	}

	void LoadScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	void Playtemp(){
		music.PlaySong(music.levelSong);
	}
}