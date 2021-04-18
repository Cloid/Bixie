using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PhotonInt : MonoBehaviour
{
    private PhotonView photonView;
    private PhotonView QinyangView;
    private PlayerInputHandler mainInput;
    private GameObject[] PlayerInputs;
    private GameObject[] Statues;
    private PhotonView StatueView;
    public GameObject Qinyang;
    // Start is called before the first frame update
    void Start()
    {
        //Finding Player Inputs
        PlayerInputs = GameObject.FindGameObjectsWithTag("PlayerInput");
        Statues = GameObject.FindGameObjectsWithTag("Statue");

        mainInput = PlayerInputs[0].GetComponent<PlayerInputHandler>();
        //Finding host PlayerInput
        if(!mainInput.hostClient){
            mainInput = PlayerInputs[1].GetComponent<PlayerInputHandler>();
        }
        //If host PlayerInput wants to play Qinyang, else play Mei-Lien
        if (mainInput.index == 0 && !PhotonNetwork.IsMasterClient)
        {
                photonView = GetComponent<PhotonView>();
                //photonView = Mei.GetComponent<PhotonView>();
                //photonView.TransferOwnership(PhotonNetwork.PlayerList[1]);
                photonView.TransferOwnership(PhotonNetwork.CurrentRoom.GetPlayer(2));
                PhotonNetwork.Instantiate("Mei Lien", new Vector3(0, 0, 0), Quaternion.identity);
                foreach(GameObject Statue in Statues){
                    StatueView = Statue.GetComponent<PhotonView>();
                    StatueView.TransferOwnership(PhotonNetwork.CurrentRoom.GetPlayer(2));
                }
                photonView.RPC("DestroySpawn", RpcTarget.AllBuffered);
        } else{
            if(PhotonNetwork.IsMasterClient){
                photonView = GetComponent<PhotonView>();
                photonView.TransferOwnership(PhotonNetwork.CurrentRoom.GetPlayer(1));
                PhotonNetwork.Instantiate("Mei Lien", new Vector3(0, 0, 0), Quaternion.identity);
                QinyangView = Qinyang.GetComponent<PhotonView>();
                Debug.Log(QinyangView);
                QinyangView.TransferOwnership(PhotonNetwork.CurrentRoom.GetPlayer(2));

                foreach(GameObject Statue in Statues){
                    StatueView = Statue.GetComponent<PhotonView>();
                    StatueView.TransferOwnership(PhotonNetwork.CurrentRoom.GetPlayer(1));
                }

                photonView.RPC("DestroySpawn", RpcTarget.AllBuffered);

            }

        }
    }

    [PunRPC]
    public void DestroySpawn()
    {
        Destroy(gameObject);
    }


}
