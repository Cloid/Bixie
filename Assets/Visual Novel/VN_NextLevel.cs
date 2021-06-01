using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class VN_NextLevel : MonoBehaviour
{
    public PhotonView photonView;
    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        if(PhotonNetwork.IsMasterClient){
            PhotonNetwork.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

}
