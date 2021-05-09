using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class introstatue : MonoBehaviour
{
    public Rigidbody rb1;
    public GameObject Torch;
    // Start is called before the first frame update
    void Update()
    {
        if(rb1.isKinematic == true && Torch.activeSelf == false){
            Torch.SetActive(true);
            // Play FMOD spawn lantern sound
            FMODUnity.RuntimeManager.PlayOneShot("event:/Sounds/Lantern_Spawn", GetComponent<Transform>().position);
        }   
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Enemy"){
            Physics.IgnoreCollision(GetComponent<Collider>(), other.collider);
        }
    }
}
