using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueCollider : MonoBehaviour
{
    /*
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }*/

void OnCollisionEnter(Collision hit)
 {
     //Source Code by MythralFTW: https://answers.unity.com/questions/1100879/push-object-in-opposite-direction-of-collision.html
     // Adapted by Miguelcloid Reniva
    Debug.Log("Hit");
     float force = .05f;
     Rigidbody hitRigidbody = hit.collider.attachedRigidbody;

     if (hit.gameObject.tag == "Player" || hit.gameObject.tag == "Enemy")
     {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX |RigidbodyConstraints.FreezePositionY | 
        RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
     }
     
    // If the object we hit is Mei Lien
     if (hit.gameObject.tag == "Player2")
     {
         GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY | 
        RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
         // Calculate Angle Between the collision point and the player
         Vector3 dir = hit.contacts[0].point - transform.position;
         // We then get the opposite (-Vector3) and normalize it
         dir = -dir.normalized;
        Debug.Log(dir);
         // And finally we add force in the direction of dir and multiply it by force. 
         // This will push back the player
        GetComponent<Rigidbody>().AddForce(dir*force);
     }

 }
 
    private void OnCollisionStay(Collision other) {
        Debug.Log("Hello?");
        if(other.gameObject.tag == "StatueWall"){
            GetComponent<Rigidbody>().isKinematic  = true;
        }
    }


}
