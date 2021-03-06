using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageController : MonoBehaviour
{
    public float minBoomerangTime, maxBoomerangTime;
    private bool isDead = false;
	private int currentHealth;
	public int maxHealth = 4;
    private Player2 player2;
    public GameObject rock_piece;
    // Start is called before the first frame update
    void Awake()
    {
        player2 = FindObjectOfType<Player2>();
        currentHealth = maxHealth; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TookDamage(int damage)
	{
		if (!isDead)
		{
			currentHealth -= damage;
			// TODO: Make anim + sound
			//anim.SetTrigger("HitDamage");
			//PlaySound(damageSound, "Damage", damage);

			if (currentHealth <= 0)
			{
				//CageBroken();
			}
		}
	}
	
	void CageBroken(){
        isDead = true;
		//TODO: Make anim for broken cage
		//anim.SetTrigger()
		Invoke("SpawnRocks", Random.Range(minBoomerangTime, maxBoomerangTime));
	}

    public void SpawnRocks(){
        Vector3 spawnLoc = player2.transform.position;
        spawnLoc.x += Random.Range(1,5);
        spawnLoc.y += Random.Range(1,5);
        spawnLoc.z = 0;
		GameObject rock = Instantiate(rock_piece, spawnLoc, transform.rotation);
    }
}
