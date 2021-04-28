using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CageController : MonoBehaviour
{
    public float minBoomerangTime, maxBoomerangTime;
    public int amountOfObjectsToSpawn = 6;
    private bool isDead = false;
	private int currentHealth;
	public int maxHealth = 4;
    private Player2 player2;
    public GameObject[] rock_pieces;
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
        //anim = GetComponent<Animator>();
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
			//anim.SetTrigger("HitDamage");
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
                Vector3 spawnLoc = player2.transform.position;
                //Add a random int 1 thru 5 at mei lien's locaiton
                spawnLoc.x += Random.Range(1,5);
                spawnLoc.y += Random.Range(1,5);
                int gameObjectIndex = Random.Range( 0, 2 );
                // GameObject rock = Instantiate(rock_pieces[gameObjectIndex], spawnLoc, transform.rotation);
                rock = Instantiate(rock_pieces[gameObjectIndex], spawnLoc, Quaternion.identity);
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
