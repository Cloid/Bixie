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
    //private bool isDead = false;
    private Animator anim_cage;
    private GameObject tempCage;
    //public int maxHealth = 10;

    // Use this for initialization
    void Awake()
    {
        player1 = FindObjectOfType<Player>();
        player2 = FindObjectOfType<Player2>();
        Mei = GameObject.Find("Mei Lien").GetComponent<Rigidbody>();
        // Invoke("SpawnCage", Random.Range(minBoomerangTime, maxBoomerangTime));
        music = FindObjectOfType<MusicController>();
        music.PlaySong(music.bossSong);
        //currentHealth = maxHealth;
    }

    private void Update()
    {
        if (tempCage != null && tempCage.activeSelf == false)
        {
			// Invoke("SpawnCage", Random.Range(minBoomerangTime, maxBoomerangTime));
        }
    }
    void SpawnCage()
    {
        if (!isDead)
        {
            tempCage = Instantiate(cage, player2.transform.position, transform.rotation);
			anim_cage = tempCage.GetComponent<Animator>();
			anim_cage.SetBool("Cage",true);
            Mei.constraints = RigidbodyConstraints.FreezeAll;
			
            //use the code below to bring mei lien back to full control;
            //Mei.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

            //if (facingRight)
            //{
            //	tempCage.GetComponent<Boomerang>().direction = 1;
            //}
            //else
            //{
            //	tempCage.GetComponent<Boomerang>().direction = -1;
            //}
            //Invoke("SpawnCage", Random.Range(minBoomerangTime, maxBoomerangTime));
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