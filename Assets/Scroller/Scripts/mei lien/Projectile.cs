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
    public SpriteRenderer currSprite;

    // Private Variables

    // Start is called before the first frame update
    void Start()
    {
        currAnim = GetComponent<Animator>();
        currSprite = GetComponent<SpriteRenderer>();
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
        switch(tag)
        {
            case "WaterWave":
                currAnim.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Assets/Scroller/Animations/Mei Lien/WaterWave.controller");
                currSprite.sprite = Resources.Load<Sprite>("");
                break;
            case "IceBall":
                currAnim.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Assets/Scroller/Animations/Mei Lien/IceBall.controller");
                break;
            default:
                currAnim.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Assets/Scroller/Animations/Mei Lien/WaterWave.controller");
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
            Destroy(gameObject);
        }
    }
}
