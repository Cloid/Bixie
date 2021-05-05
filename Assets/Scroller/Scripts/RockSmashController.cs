using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class RockSmashController : MonoBehaviour
{
    public PhotonView photonView;
    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    [PunRPC]
    void DestroyRock(){
        Destroy(gameObject);
    }
}
