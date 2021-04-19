using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class LoadSceneProxy : MonoBehaviour
{
    public GameObject cc_control;
    private CS_Control cs_Script;
    // Start is called before the first frame update
    void Start()
    {
        cs_Script = cc_control.GetComponent<CS_Control>();
        if(PhotonNetwork.IsMasterClient && cs_Script != null && cs_Script.loadVar == true){
            PhotonNetwork.LoadLevel("Scroller_1_1");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
