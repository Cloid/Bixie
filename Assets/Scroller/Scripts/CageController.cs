using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class CageController : MonoBehaviour
{
    public float minBoomerangTime, maxBoomerangTime;
    public int amountOfObjectsToSpawn = 1;
    private bool isDead = false;
	private int currentHealth;
	public int maxHealth = 4;
    private Player2 player2;
    public GameObject[] rock_pieces;
    public Transform[] spawnPoints;
    private GameObject rock;

    private Animator anim;

    private GameObject boss;
    private Rigidbody Mei;
    // Start is called before the first frame update
    void Awake()
    {
        player2 = FindObjectOfType<Player2>();
        currentHealth = maxHealth;
        Mei = GameObject.Find("Mei Lien").GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        spawnPoints = new Transform[2];
        
        
        //Debug.Log(anim); 
        //anim.SetTrigger("Cage");
    }

    // Update is called once per fram

    public void TookDamage(int damage)
	{
		if (!isDead)
		{
            Debug.Log("Current Health: " + currentHealth);
			currentHealth -= damage;
			// TODO: Make anim + sound
            // anim.SetBool("Cage", false);
			anim.SetTrigger("Hitdamage");
			//PlaySound(damageSound, "Damage", damage);

			if (currentHealth <= 0)
			{
				CageBroken();
			}
		}
	}
	
	void CageBroken(){
        isDead = true;
        Mei.constraints = RigidbodyConstraints.FreezeRotation;
        
		//TODO: Make anim for broken cage
		//anim.SetTrigger()
		// Invoke("SpawnRocks", Random.Range(minBoomerangTime, maxBoomerangTime));
        SpawnRocks();
	}

    public void SpawnRocks(){
        //As long as player2 is not null proceed w/ function, else find player and call function again
        if(player2 != null){
            
            for (int i = 0; i < amountOfObjectsToSpawn; i ++)
            {
                // Vector3 spawnLoc = player2.transform.position;
                //Add a random int 1 thru 5 at mei lien's locaiton
                // spawnLoc.x += Random.Range(1,5);
                // spawnLoc.y += Random.Range(1,5);
                int gameObjectIndex = Random.Range( 0, 2 );
                int spawnIndex = Random.Range(0, spawnPoints.Length);
                // GameObject rock = Instantiate(rock_pieces[gameObjectIndex], spawnLoc, transform.rotation);
                var rock_copy = rock_pieces[gameObjectIndex].gameObject;
                spawnPoints[0] = rock_copy.gameObject.transform;
                spawnPoints[0].position = new Vector3(-4.4000001f,-0.74000001f,-3.1099999f);
                spawnPoints[1] = rock_copy.gameObject.transform;
                spawnPoints[1].position = new Vector3(-4.4000001f,-0.0299999993f,3.57999992f);
                rock =  PhotonNetwork.Instantiate(rock_copy.name,spawnPoints[spawnIndex].position,transform.rotation); //Instantiate(rock_pieces[gameObjectIndex], spawnPoints[spawnIndex].position, Quaternion.identity);
            }
            
            Destroy(gameObject);
            // if(rock.activeSelf){
            //     Destroy(gameObject);
            // }
        } else {
            player2 = FindObjectOfType<Player2>();
            SpawnRocks();
        }
        
    }
}
