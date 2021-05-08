using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpFix : MonoBehaviour
{
    public GameObject GroundCheck;
    public MeshRenderer MeiShadow;
    public Vector3 ogPos;
    // Start is called before the first frame update
    void Start()
    {
        ogPos = GroundCheck.transform.position;
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.name == "Mei Lien"){
            Debug.Log("Test");
            GroundCheck.transform.position = ogPos;
            MeiShadow.enabled = true;
        }
    }
}
