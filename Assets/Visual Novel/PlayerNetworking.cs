using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerNetworking : MonoBehaviourPunCallbacks

{
    public GameObject PlayerInput;
    public GameObject MeiLien;
    private PhotonView photonView;
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        //PhotonNetwork.OfflineMode = true;

    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        //base.OnConnectedToMaster();
    }

    public override void OnJoinedLobby()
    {
        //base.OnJoinedLobby();
        PhotonNetwork.JoinOrCreateRoom("Room2", new Photon.Realtime.RoomOptions() {MaxPlayers = 2}, typedLobby:default);
    }

    public override void OnJoinedRoom()
    {
        //base.OnJoinedRoom();
        Debug.Log("Joined");
        //Debug.Log(PhotonNetwork.CurrentRoom.GetPlayer(2));
        PhotonNetwork.Instantiate(PlayerInput.name, new Vector3(0,0,0), Quaternion.identity);
        
        if(PhotonNetwork.PlayerList.Length==2){
            //photonView = Mei.GetComponent<PhotonView>();
            //photonView.TransferOwnership(PhotonNetwork.PlayerList[1]);
            PhotonNetwork.Instantiate("Mei Lien", new Vector3(0,0,0), Quaternion.identity);
        }
        //if conditional for offline
        
    }
}
