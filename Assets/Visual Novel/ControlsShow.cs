using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;
public class ControlsShow : MonoBehaviour
{
    public GameObject controls;
    public InputAction wasd;
    public PhotonView photonView;

    public GameObject loadObject;

    public bool loadVar = false;

    public CS_Control cS;

    public PlayerInputHandler[] playerInputHandlers;
    public PlayerInputHandler p1;
    public PlayerInputHandler p2;
    // Start is called before the first frame update
    void Start()
    {
        controls.SetActive(true);
        photonView = GetComponent<PhotonView>();
        cS = FindObjectOfType<CS_Control>();
        //TorchControls = (TorchControllerSS[])GameObject.FindObjectsOfType(typeof(TorchControllerSS));
        playerInputHandlers = (PlayerInputHandler[])GameObject.FindObjectsOfType(typeof(PlayerInputHandler));
        foreach(PlayerInputHandler playerInputHandler in playerInputHandlers){
            if(playerInputHandler.index == 0){
                p1 = playerInputHandler;
            } 
            else{
                p2 = playerInputHandler;
            }
        }

        if(cS.p1index == 0){
            p1.index = 1;
            p2.index = 0;
        }

    }

    void OnEnable()
    {
        wasd.Enable();
    }

    void OnDisable()
    {
        wasd.Disable();
    }

    [PunRPC]
    public void startOnline(){
        Debug.Log("Am here");
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("Work?");
            loadObject.SetActive(true);
        }
    }
    private void Update()
    {
        Keyboard kb = InputSystem.GetDevice<Keyboard>();
        Gamepad gp = InputSystem.GetDevice<Gamepad>();

        if (gp == null)
        {

            if (kb.enterKey.wasPressedThisFrame || kb.spaceKey.wasPressedThisFrame)
            {
                Debug.Log("work");
                photonView.RPC("startOnline", RpcTarget.AllBuffered);
            }

        }
        else
        {
            if (kb.enterKey.wasPressedThisFrame ||
            kb.spaceKey.wasPressedThisFrame || gp.aButton.wasPressedThisFrame || gp.buttonEast.wasPressedThisFrame)
            {
                Debug.Log("work2");
                photonView.RPC("startOnline", RpcTarget.AllBuffered);
            }


        }
    }

}
