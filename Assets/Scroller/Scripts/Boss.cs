using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : Enemy {

	public GameObject boomerang;
	public float minBoomerangTime, maxBoomerangTime;

	private MusicController music;

	// Use this for initialization
	void Awake () {
		Invoke("ThrowBoomerang", Random.Range(minBoomerangTime, maxBoomerangTime));
		music = FindObjectOfType<MusicController>();
		music.PlaySong(music.bossSong);
	}
	
	void ThrowBoomerang()
	{
		if (!isDead)
		{
			anim.SetTrigger("Boomerang");
			GameObject tempBoomerang = Instantiate(boomerang, transform.position, transform.rotation);
			if (facingRight)
			{
				tempBoomerang.GetComponent<Boomerang>().direction = 1;
			}
			else
			{
				tempBoomerang.GetComponent<Boomerang>().direction = -1;
			}
			Invoke("ThrowBoomerang", Random.Range(minBoomerangTime, maxBoomerangTime));
		}
	}

	void BossDefeated()
	{
		music.PlaySong(music.levelClearSong);
		FindObjectOfType<UIManager>().UpdateDisplayMessage("Level Clear");
		FindObjectOfType<ResetCameraScript>().Activate();
		Invoke("Playtemp", 8f);
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
