using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityWall : MonoBehaviour
{
    public GameObject Pushey;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Statue"){
        var statuePos = other.gameObject.GetComponent<Transform>().position;  
        statuePos.y = Pushey.gameObject.transform.position.y;
        statuePos.z = Pushey.gameObject.transform.position.z;
        if(Pushey.gameObject.name == "RoofWall"){
            statuePos.x = Pushey.gameObject.transform.position.x-1;
        } else if(Pushey.gameObject.name == "RoofWall2"){
        statuePos.x = Pushey.gameObject.transform.position.x+1;
        }else{
         statuePos.x = Pushey.gameObject.transform.position.x-5;
        }
        other.gameObject.GetComponent<Transform>().position = statuePos;                    
        Rigidbody rb = other.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionX | 
        RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            rb.useGravity = true;
        }
    }
}
