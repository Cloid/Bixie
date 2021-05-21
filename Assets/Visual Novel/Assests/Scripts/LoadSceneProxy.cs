using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class LoadSceneProxy : MonoBehaviour
{
    public GameObject cc_Show;
    private ControlsShow cs_Script;
    // Start is called before the first frame update
    void Start()
    {
        cs_Script = cc_Show.GetComponent<ControlsShow>();
        if(PhotonNetwork.IsMasterClient && cs_Script != null && cs_Script.loadVar == true){
            Debug.Log("What now");
            PhotonNetwork.LoadLevel("Scroller_1_1");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
