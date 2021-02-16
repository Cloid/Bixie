using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PusheyCollider : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody rb;

    public void OnTriggerEnter(Collider other) {
        Debug.Log("working?");
        if(other.gameObject.tag == "Statue"){
            Debug.Log("Working");
            rb = other.gameObject.GetComponent<Rigidbody>();
            rb.isKinematic = true;
        }
    }
}
