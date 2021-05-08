using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
public class StatueCollider : MonoBehaviour
{
    private PhotonView photonView;

    // event insttance variable for fmod
    private static FMOD.Studio.EventInstance stone_sound;
    private Vector3 xPush;
    private SpriteRenderer currSprite;

    
    /*
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }*/
private void Start() {
    photonView = GetComponent<PhotonView>();
    currSprite = GetComponent<SpriteRenderer>();

    //currSprite.color = Color.green;
    //xPush = new Vector3(4,2,-2);
    //Invoke("fuckMe", 5.0f);
    //photonView.TransferOwnership(PhotonNetwork.CurrentRoom.GetPlayer(2));
    //StartCoroutine(test());
    //Debug.Log("TEST");
}


public void PushingStatue(float attackDir){
        //GetComponent<Rigidbody>().AddForce(transform.forward,ForceMode.VelocityChange);
        Debug.Log("test2");
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY | 
        RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        // made statue push a little stronger
        //Vector3 work = new Vector3(30*attackDir,0,0);
        Vector3 work = new Vector3(90*attackDir,0,0);
        GetComponent<Rigidbody>().AddForce(work, ForceMode.Impulse);
        // Start playing the grinding sound
        //stone_sound = FMODUnity.RuntimeManager.CreateInstance("event:/Sounds/Stone_Grind");
        //FMODUnity.RuntimeManager.AttachInstanceToGameObject(stone_sound, GetComponent<Transform>(), GetComponent<Rigidbody>());
        //stone_sound.start();
        //stone_sound.release();

}


void OnCollisionEnter(Collision hit)
 {
    //Source Code by MythralFTW: https://answers.unity.com/questions/1100879/push-object-in-opposite-direction-of-collision.html
    // Adapted by Miguelcloid Reniva
    // Debug.Log("Hit");

     float force = .05f;
     Rigidbody hitRigidbody = hit.collider.attachedRigidbody;

     if (hit.gameObject.tag == "Player" || hit.gameObject.tag == "Player2" || hit.gameObject.tag == "Enemy")
     {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX |RigidbodyConstraints.FreezePositionY | 
        RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
     }

     if(SceneManager.GetActiveScene().buildIndex == 5 && hit.gameObject.tag == "Player"){
         GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
     }
     
     
    // If the object we hit is Mei Lien
    /*
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

        // Start playing the grinding sound
        stone_sound = FMODUnity.RuntimeManager.CreateInstance("event:/Sounds/Stone_Grind");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(stone_sound, GetComponent<Transform>(), GetComponent<Rigidbody>());
        stone_sound.start();
        stone_sound.release();
     }*/

     if(hit.gameObject.tag == "StatueWall"){
            //Debug.Log("Hello!!!!");
            //GetComponent<Renderer>().material.color = Color.green;

                        //Debug.Log("I ran");

            GetComponent<Rigidbody>().isKinematic  = true;
            //GetComponent<SpriteRenderer>().color = Color.black;
        }

 }

void OnCollisionExit(Collision hit)
{
    if (hit.gameObject.tag == "Player2")
    {
        stone_sound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    } 
}
 

}
