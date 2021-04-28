using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class Offline_Networking : MonoBehaviourPunCallbacks

{
    public GameObject pInput;
    public GameObject PhotonInt;
    //private PhotonView photonView;
    private void Awake() {
        PhotonInt.SetActive(false);
    }
    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.OfflineMode = true;
 
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinOrCreateRoom("offline", new Photon.Realtime.RoomOptions() {MaxPlayers = 2}, typedLobby:default);
        
        //base.OnConnectedToMaster();
    }

    public override void OnJoinedRoom()
    {
        //base.OnJoinedRoom();
        Debug.Log("Joined");
        // Debug.Log("Test2: "+PhotonNetwork.OfflineMode);
        pInput.SetActive(true);
        
    }

    
}
