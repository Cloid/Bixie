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

	// Use this for initialization
	void Awake()
	{
		player1 = FindObjectOfType<Player>();
		player2 = FindObjectOfType<Player2>();
		Invoke("SpawnCage", Random.Range(minBoomerangTime, maxBoomerangTime));
		music = FindObjectOfType<MusicController>();
		music.PlaySong(music.bossSong);
	}

	void SpawnCage()
	{
		if (!isDead)
		{
			anim.SetTrigger("Boomerang");
			GameObject tempCage = Instantiate(cage, player2.transform.position, transform.rotation);
			//if (facingRight)
			//{
			//	tempCage.GetComponent<Boomerang>().direction = 1;
			//}
			//else
			//{
			//	tempCage.GetComponent<Boomerang>().direction = -1;
			//}
			Invoke("SpawnCage", Random.Range(minBoomerangTime, maxBoomerangTime));
		}
	}

	void BossDefeated()
	{
		music.PlaySong(music.levelClearSong);
		FindObjectOfType<UIManager>().UpdateDisplayMessage("Level Clear");
		//FindObjectOfType<ResetCameraScript>().Activate();
		//Invoke("Playtemp", 8f);
		// Invoke("LoadScene", 8f);
	}

	void LoadScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	void Playtemp()
	{
		music.PlaySong(music.levelSong);
	}
}