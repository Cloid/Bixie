using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Photon_Lit : MonoBehaviour
{
    public PhotonView photonView;
    public TorchControllerSS[] TorchControls;
    Coroutine lastRoutine = null;

    public GameObject EventSystem;

    public Player2 player2;
    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        player2 = FindObjectOfType<Player2>();
        TorchControls = (TorchControllerSS[])GameObject.FindObjectsOfType(typeof(TorchControllerSS));
        //EventSystem = GameObject.Find("EventSystem");
        //testMe();
    }

    private void Update() {
        TorchControls = (TorchControllerSS[])GameObject.FindObjectsOfType(typeof(TorchControllerSS));
    }

    [PunRPC]
    public void litLantern()
    {
        Debug.Log("here");
        foreach (TorchControllerSS TorchControl in TorchControls)
        {
            if (TorchControl.isMeiInside)
            {
                print("started ritual");
                lastRoutine = StartCoroutine(TorchControl.lightLantern());
                player2.isLighting = true;
                player2.rb.constraints = RigidbodyConstraints.FreezeAll;
            }

        }
    }

    [PunRPC]
    public void unlitLantern()
    {
        foreach (TorchControllerSS TorchControl in TorchControls)
        {
            TorchControl.disableLantren();
            if (lastRoutine != null) StopCoroutine(lastRoutine);
            player2.isLighting = false;
            player2.rb.constraints = RigidbodyConstraints.FreezeRotation;
        }
    }

}
