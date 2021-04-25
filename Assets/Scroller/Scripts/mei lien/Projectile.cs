using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Public Variables
    public float lifespan;
    public float attackDir;

    // Private Variables

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        // Checks lifespan of projectile, if it is reaches 0 or below then it is destroyed
        if(lifespan <= 0)
        {
            Debug.Log("Projectile is destroyed!");
            Destroy(gameObject);
        } else
        {
            lifespan -= Time.deltaTime;
        }
    }

    // Collision check with enemy 
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Projectile has collided!");
        Enemy enemy = other.GetComponent<Enemy>();
        if(enemy != null)
        {
            Debug.Log("Projectile collided with enemy!");
            enemy.TookDamage(0, "meiLienBasicAttack", attackDir);
            Debug.Log(attackDir);
            Destroy(gameObject);
        } else if (other.gameObject.tag == "Statue"){
            StatueCollider statue = other.GetComponent<StatueCollider>();
            Debug.Log(attackDir);
            statue.PushingStatue(attackDir);
            Destroy(gameObject);
        }
    }

}
