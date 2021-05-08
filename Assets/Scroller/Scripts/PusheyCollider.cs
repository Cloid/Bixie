using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PusheyCollider : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody rb;
    public GameObject Wall;

    public void OnTriggerEnter(Collider other) {
        //Debug.Log("working?");
        if(other.gameObject.tag == "Statue"){
            Debug.Log("Working");

            // Play stone pad pushed sound
            FMODUnity.RuntimeManager.PlayOneShot("event:/Sounds/Stone_Pushed", GetComponent<Transform>().position);

            rb = other.gameObject.GetComponent<Rigidbody>();

            // snap statue onto base
            var statuePos = other.gameObject.GetComponent<Transform>().position;
            statuePos.x = transform.position.x;
            other.gameObject.GetComponent<Transform>().position = statuePos;

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
