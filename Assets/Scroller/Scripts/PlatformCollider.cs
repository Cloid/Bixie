using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCollider : MonoBehaviour
{
    public GameObject Mei;
    public GameObject GroundCheck;
    public MeshRenderer MeiShadow;
    private BoxCollider collider;
    private Vector3 ogPos;
    private bool platHeight;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider>();
        //GroundCheck = GameObject.Find("GroundCheck2");
        ogPos = GroundCheck.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // if(Mei.transform.position.y < 2.4 && Mei.transform.position.y > 2.2){
        //     Debug.Log("Am here");
        //     //collider.enabled = true;
        //     //Mei.GetComponent<Player2>().onGround2 = true;
        //     Vector3 pos = GroundCheck.transform.position;
        //     pos.y -= 1;
        //     GroundCheck.transform.position = pos;
            
        // } else {
        //     Vector3 pos = GroundCheck.transform.position;
        //     //pos.y += 1;
        //     GroundCheck.transform.position = ogPos;

        // }
    }
    
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Player2"){
            //GroundCheck.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,0f);
            MeiShadow.enabled = false;
        }
    }

    private void OnCollisionStay(Collision other) {
        //Debug.Log("Tdfgdf");
        if(other.gameObject.tag == "Player2"){
        MeiShadow.enabled = false;
        //GroundCheck.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,0f);
        Vector3 pos = GroundCheck.transform.position;
        pos.y = -4;
        GroundCheck.transform.position = pos;
        }
    }

    private void OnCollisionExit(Collision other) {
        Debug.Log("I RAN");
        GroundCheck.transform.position = ogPos;    
    }
}
