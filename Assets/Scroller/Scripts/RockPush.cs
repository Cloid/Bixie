using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockPush : MonoBehaviour
{
    public float stunTime;
    private Rigidbody Boss1_body;
    private Shanxiao_BossACT1 Boss1_script;
    private Animator Boss1_anim;

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
        if (other.gameObject.tag == "Boss1")
        {
            StartCoroutine(stun());
            // print("this should be called after wait");
            // Destroy(gameObject);
            // move.enabled=false;
            // touche = true;
            
 
        }
 
    }

    IEnumerator stun()
    {
        Debug.Log ("with rock boss");
        // Shanxiao_BossACT1 moveScript = other.GetComponent<Shanxiao_BossACT1>();
        Boss1_body.constraints = RigidbodyConstraints.FreezeAll;
        Boss1_script.enabled = false;
        Boss1_anim.enabled = false;
        yield return new WaitForSeconds(stunTime);
        
        Boss1_body.constraints = RigidbodyConstraints.FreezeRotation;
        Boss1_script.enabled = true;
        Boss1_anim.enabled = true;
        Destroy(gameObject);
        
    }

    // IEnumerator returnattente ()
    // {
    //     yield return attente;
    // }
}
