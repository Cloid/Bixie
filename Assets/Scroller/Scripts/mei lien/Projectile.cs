using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Public Variables
    public float lifespan;
    public float attackDir;
    public string projTag;
    public Animator currAnim;

    // Private Variables

    // Awake is called before the first frame
    void Awake()
    {
        currAnim = GetComponent<Animator>();
        projSprite(projTag);
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

    
    // Helper function that determines animation and sprites of a projectile 
    public void projSprite(string tag)
    {
        print(tag);
        switch(tag)
        {
            case "WaterWave":
                currAnim.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("WaterWave");
                projTag = "WaterWave";
                break;
            case "IceBall":
                currAnim.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("IceBall");
                projTag = "IceBall";
                break;
            default:
                currAnim.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("WaterWave");
                projTag = "WaterWave";
                break;
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
            switch (projTag)
            {
                case "WaterWave":
                    enemy.TookDamage(0, "meiLienBasicAttack", projTag, attackDir);
                    break;
                case "IceBall":
                    enemy.TookDamage(1, "meiLienHeavyAttack", projTag, attackDir);
                    break;
                default:
                    enemy.TookDamage(0, "meiLienBasicAttack", projTag, attackDir);
                    break;
              }
            //Debug.Log(attackDir);
            Invoke("destroyObject",0.1f);
        } else if (other.gameObject.tag == "Statue"){
            StatueCollider statue = other.GetComponent<StatueCollider>();
            Debug.Log(attackDir);
            statue.PushingStatue(attackDir);
            Destroy(gameObject);
        }
    }

    void destroyObject()
    {
        Destroy(gameObject);
    }
}

