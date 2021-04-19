using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class PhotonProxy : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject PhotonProxy_get;
    private PhotonGetter photonGetter;
    public Flowchart PhotonMenu;
    public int num = 0;
    private GameObject Op;
    void Start()
    {
        Op = GameObject.Find("AudioController");
       PhotonProxy_get = GameObject.Find("AudioController");
       photonGetter = PhotonProxy_get.GetComponent<PhotonGetter>();

        if(photonGetter.error){
            PhotonMenu.StopAllBlocks();
            PhotonMenu.ExecuteBlock("ErrorJoin");
        } else if(photonGetter.errorCreate){
            PhotonMenu.StopAllBlocks();
            PhotonMenu.ExecuteBlock("ErrorCreate");
        }
        

    }

    public void changeOptions(){


        //Op.SetActive(false);

        if(Op.activeSelf){
            Op.SetActive(false);
        } else {
            Op.SetActive(true);
        }
        
    }

    public void changeLocal(){
        photonGetter.changeLocal();
    }

    public void changeLobby1(){
        photonGetter.changeLobby1();
    }

    public void changeLobby2(){
        photonGetter.changeLobby2();
    }

    public void changeLobby3(){
        photonGetter.changeLobby3();
    }

    public void PhotonMenuPop(){
        PhotonMenu.StopAllBlocks();
        PhotonMenu.ExecuteBlock("Yes (option)");
    }

    public void updateLobbies(){
        num = PhotonMenu.GetIntegerVariable("lobby1");
    }

}
