using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreEnemyCollider : MonoBehaviour
{
    // Start is called before the first frame update
   
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Enemy"){
            Physics.IgnoreCollision(GetComponent<Collider>(), other.collider);
        }
    }
}
