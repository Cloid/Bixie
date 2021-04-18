using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonGetter : MonoBehaviour
{
    //Make a getter and setter function for this
    public bool local = false;
    public string lobby; 
    // Start is called before the first frame update
    
    public void changeLocal(){
        local = true;
    }

    public void changeLobby1(){
        lobby = "1";
    }

    public void changeLobby2(){
        lobby = "2";
    }

    public void changeLobby3(){
        lobby = "3";
    }

}
