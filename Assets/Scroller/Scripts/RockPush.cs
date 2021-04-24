using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockPush : MonoBehaviour
{
    private Rigidbody Boss1;

    // Start is called before the first frame update
    void Start()
    {
        Boss1 = GameObject.Find("Shanxiao_ACT1Boss").GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.tag == "Boss1")
        {
            // Shanxiao_BossACT1 moveScript = other.GetComponent<Shanxiao_BossACT1>();
            Boss1.constraints = RigidbodyConstraints.FreezeAll;
            attente();
            // move.enabled=false;
            // touche = true;
            // Debug.Log ("Il y a eu collision");
 
        }
 
    }

    IEnumerator attente ()
    {
        Boss1.constraints = RigidbodyConstraints.FreezeRotation;
        yield return new WaitForSeconds (3);
    }

    // IEnumerator returnattente ()
    // {
    //     yield return attente;
    // }
}
