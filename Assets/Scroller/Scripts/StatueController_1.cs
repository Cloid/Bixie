using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueController_1 : MonoBehaviour
{
    public Rigidbody rb1;
    public Rigidbody rb2;
    public GameObject Torch;
    public GameObject Lua;

    // Update is called once per frame
    void Update()
    {
        if(rb1.isKinematic == true && rb2.isKinematic == true && Torch.activeSelf == false){
            Torch.SetActive(true);
            Lua.SetActive(true);

            // Play FMOD spawn lantern sound
            FMODUnity.RuntimeManager.PlayOneShot("event:/Sounds/Lantern_Spawn", GetComponent<Transform>().position);
        }   
    }
}
