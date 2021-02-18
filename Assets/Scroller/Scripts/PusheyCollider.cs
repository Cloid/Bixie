using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PusheyCollider : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody rb;
    public GameObject Wall;

    public void OnTriggerEnter(Collider other) {
        Debug.Log("working?");
        if(other.gameObject.tag == "Statue"){
            Debug.Log("Working");
            rb = other.gameObject.GetComponent<Rigidbody>();
            rb.isKinematic = true;
            if(rb.isKinematic == true){
                Debug.Log("its works");
                if(Wall != null){
                    Color tmp = Wall.GetComponent<SpriteRenderer>().color;
                    tmp.a = tmp.a * 0.9f;
                    Wall.GetComponent<SpriteRenderer>().color = tmp;
                }          
            }
        }
    }
}
