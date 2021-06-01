using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
public class Photon_VNSync : MonoBehaviour
{
    public PhotonView photonView;
    public bool p1_done = false;
    public bool p2_done = false;
    public VN_NextLevel next;
    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        next = GetComponent<VN_NextLevel>();
    }

    // Update is called once per frame
    void Update()
    {
        if(PhotonNetwork.IsMasterClient && p2_done && p1_done){
            next.enabled = true;
            //PhotonNetwork.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    [PunRPC]
    public void changep1Bool(){
        p1_done = true;
    }

    [PunRPC]
    public void changep2Bool(){
        p2_done = true;
    }

    public void photonp1(){
        //Photon.Pun
        photonView.RPC("changep1Bool", RpcTarget.AllBuffered);
    }

    public void photonp2(){
        photonView.RPC("changep2Bool", RpcTarget.AllBuffered);
    }

    public void decideBool(){

        if(PhotonNetwork.OfflineMode){
            PhotonNetwork.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
        }

        if(PhotonNetwork.IsMasterClient){
            photonp1();
        } else {
            photonp2();
        }
    }

}
