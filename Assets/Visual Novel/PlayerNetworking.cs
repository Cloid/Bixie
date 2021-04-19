using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class PlayerNetworking : MonoBehaviourPunCallbacks

{
    private GameObject PhotonProxy_get;
    private PhotonGetter photonGetter;
    public GameObject PManager;
    public GameObject photon_p1;
    public GameObject photon_p2;
    //private PhotonView photonView;
    void Start()
    {
        PhotonProxy_get = GameObject.Find("AudioController");
        photonGetter = PhotonProxy_get.GetComponent<PhotonGetter>();
        PhotonNetwork.AutomaticallySyncScene = true;
        //Checking if variable from other is true, then assign conditions for local co-op
        if(photonGetter.local==true){

            if(string.IsNullOrEmpty(photonGetter.lobby)){
                Debug.Log("test");
                photonGetter.lobby = "x";
            }

            //PhotonNetwork.OfflineMode = true;
            Debug.Log("Test: "+PhotonNetwork.OfflineMode);
            PhotonNetwork.OfflineMode = true;
        } else{
            PhotonNetwork.ConnectUsingSettings();
        }        
    }

    public override void OnConnectedToMaster()
    {
        if(PhotonNetwork.OfflineMode){
            Debug.Log("testing offline");
            PhotonNetwork.JoinOrCreateRoom(photonGetter.lobby, new Photon.Realtime.RoomOptions() {MaxPlayers = 2}, typedLobby:default);
        } else{
        PhotonNetwork.JoinLobby();
        }
        //base.OnConnectedToMaster();
    }

    public override void OnJoinedLobby()
    {
        //base.OnJoinedLobby();
        PhotonNetwork.JoinOrCreateRoom(photonGetter.lobby, new Photon.Realtime.RoomOptions() {MaxPlayers = 2}, typedLobby:default);
    }

    public override void OnJoinedRoom()
    {
        //base.OnJoinedRoom();
        Debug.Log("Joined");
        Debug.Log("Test2: "+PhotonNetwork.OfflineMode);
        //Debug.Log(PhotonNetwork.CurrentRoom.GetPlayer(2));
        if(PhotonNetwork.OfflineMode == true){
            Debug.Log("IN HERE");
            //PhotonNetwork.Instantiate("Player Input Manager", new Vector3(0,0,0), Quaternion.identity);
            PManager.SetActive(true);
            //PhotonNetwork.Instantiate("PlayerInput", new Vector3(0,0,0), Quaternion.identity);

            //PhotonNetwork.Instantiate("Mei Lien", new Vector3(0,0,0), Quaternion.identity);
        }else{
        PhotonNetwork.Instantiate("PlayerInput", new Vector3(0,0,0), Quaternion.identity);
            if(PhotonNetwork.PlayerList.Length == 2){
                photon_p2.SetActive(true);
            } else{
                photon_p1.SetActive(true);
            }
        }
        
        //if conditional for offline
        
    }
    public override void OnJoinRoomFailed(short returnCode, string message){
        //Debug.Log("Shit");
        PhotonNetwork.Disconnect();
        GameObject.FindObjectOfType<PhotonGetter>().error = true;
        SceneManager.LoadScene("IntroVN");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        //base.OnCreateRoomFailed(returnCode, message);
        PhotonNetwork.Disconnect();
        GameObject.FindObjectOfType<PhotonGetter>().errorCreate = true;
        SceneManager.LoadScene("IntroVN");

    }

    
}
