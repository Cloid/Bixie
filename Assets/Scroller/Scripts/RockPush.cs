using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockPush : MonoBehaviour
{
    public float stunTime;
    private Rigidbody Boss1_body;
    private Shanxiao_BossACT1 Boss1_script;
    private Animator Boss1_anim;
    private GameObject wave;

    // Start is called before the first frame update
    void Start()
    {
        Boss1_body = GameObject.Find("Shanxiao_ACT1Boss").GetComponent<Rigidbody>();
        Boss1_script = GameObject.Find("Shanxiao_ACT1Boss").GetComponent<Shanxiao_BossACT1>();
        Boss1_anim = GameObject.Find("Shanxiao_ACT1Boss").GetComponent<Animator>();
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter (Collider other)
    {
        float force = .05f;
        if (other.gameObject.tag == "Boss1")
        {
            StartCoroutine(stun());
            // print("this should be called after wait");
            // Destroy(gameObject);
            // move.enabled=false;
            // touche = true;
            
 
        }

        if (other.gameObject.tag == "MeiWave")
        {
            /*
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY | 
            RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            // Calculate Angle Between the collision point and the player
            Vector3 dir = other.contacts[0].point - transform.position;
            // We then get the opposite (-Vector3) and normalize it
            dir = -dir.normalized;
            Debug.Log(dir);
            // And finally we add force in the direction of dir and multiply it by force. 
            // This will push back the player
            GetComponent<Rigidbody>().AddForce(dir*force);

            // Start playing the grinding sound
            // stone_sound = FMODUnity.RuntimeManager.CreateInstance("event:/Sounds/Stone_Grind");
            // FMODUnity.RuntimeManager.AttachInstanceToGameObject(stone_sound, GetComponent<Transform>(), GetComponent<Rigidbody>());
            // stone_sound.start();
            // stone_sound.release();
            */
            wave = GameObject.FindGameObjectWithTag("MeiWave");
            Debug.Log("rock has collided!");
            Debug.Log(transform.position);
            bool facingRight = (wave.transform.position.x < transform.position.x) ? false : true;
            if(facingRight){
                transform.position = new Vector3(transform.position.x - 8, transform.position.y, transform.position.z);
            }else{
                transform.position = new Vector3(transform.position.x + 8, transform.position.y, transform.position.z);
            }
        }
 
    }

    IEnumerator stun()
    {
        Debug.Log ("with rock boss");
        // Shanxiao_BossACT1 moveScript = other.GetComponent<Shanxiao_BossACT1>();
        Boss1_body.constraints = RigidbodyConstraints.FreezeAll;
        Boss1_script.enabled = false;
        // Boss1_anim.enabled = false;
        Boss1_anim.SetBool("isStun", true);
        yield return new WaitForSeconds(stunTime);
        
        Boss1_body.constraints = RigidbodyConstraints.FreezeRotation;
        Boss1_script.enabled = true;
        // Boss1_anim.enabled = true;
        Boss1_anim.SetBool("isStun", false);
        Destroy(gameObject);
        
    }

    // IEnumerator returnattente ()
    // {
    //     yield return attente;
    // }
}
