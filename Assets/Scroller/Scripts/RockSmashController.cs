using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class RockSmashController : MonoBehaviour
{
    public PhotonView photonView;
    public int health = 3;
    private SpriteRenderer currSprite;

    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        currSprite = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    [PunRPC]
    void DestroyRock(){
        health-=1;
        currSprite.color = Color.blue;
        StartCoroutine(whitecolor());
		//yield return new WaitForSeconds(1);
        if(health==0){
            Destroy(gameObject);
        }
    }
    IEnumerator whitecolor() {
        yield return new WaitForSeconds(0.6f);
        GetComponent<SpriteRenderer> ().color = Color.white;
    }
}
