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
    // Start is called before the first frame update
    void Start()
    {
        controls.SetActive(true);
        photonView = GetComponent<PhotonView>();
    }

    void OnEnable()
    {
        wasd.Enable();
    }

    void OnDisable()
    {
        wasd.Disable();
    }
    private void Update()
    {
        Keyboard kb = InputSystem.GetDevice<Keyboard>();
        Gamepad gp = InputSystem.GetDevice<Gamepad>();

        if (gp != null && PhotonNetwork.IsMasterClient)
        {

            if (kb.enterKey.wasPressedThisFrame || kb.spaceKey.wasPressedThisFrame)
            {
                Debug.Log("Worked");
                loadVar = true;
            }

        }
        else
        {
            if (PhotonNetwork.IsMasterClient && kb.enterKey.wasPressedThisFrame ||
            kb.spaceKey.wasPressedThisFrame || gp.aButton.wasPressedThisFrame || gp.buttonEast.wasPressedThisFrame)
            {
                                Debug.Log("Worked!!");

                loadVar = true;
            }


        }

        if (PhotonNetwork.IsMasterClient && loadVar)
        {
                            Debug.Log("Worked!!!");

            loadObject.SetActive(true);
        }

    }

}
