using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
public class CS_Control : MonoBehaviour
{
    public GameObject ml_sel;
    public GameObject ml_unsel;
    public GameObject q_sel;
    public GameObject q_unsel;
    public GameObject blur;
    public GameObject[] myObj;
    public GameObject p2_text;
    public GameObject ctext;
    public GameObject controls;
    public GameObject sceneTransition;
    private PlayerInputHandler p1;
    private PlayerInputHandler p2;
    private CameraShake goCam;
    private bool ml_on = false;
    private bool q_on = false;
    public bool p1_selected = false;
    public GameObject photonc_text;
    public GameObject connected_text;
    public GameObject connection_p2_text;
    public GameObject photon_controls;
    public GameObject loadObject;
    private GameObject PhotonProxy_get;
    private PhotonGetter photonGetter;
    private PhotonView photonView;
    public bool loadVar = false;
    private float p1index = 1;
    private float p2index = 1;

    private bool runOnce = false;
    public GameObject p1_none;
    public GameObject p1_ML;
    public GameObject p1_Q;
    public GameObject p2_none;
    public GameObject p2_ML;
    public GameObject p2_Q;
    public GameObject controlShow;
    public GameObject continueIcon;
    private bool controlbool = false;
    private bool lockedIn = false;


    // Start is called before the first frame update
    void Start()
    {
        PhotonProxy_get = GameObject.Find("AudioController");
        photonGetter = PhotonProxy_get.GetComponent<PhotonGetter>();

        if (photonGetter.local == false)
        {
            //ctext.SetActive(false);
            //photonc_text.SetActive(true);
            photonView = GetComponent<PhotonView>();
            //photonView.TransferOwnership(1);
        }

        goCam = gameObject.GetComponent<CameraShake>();
    }

    private void Update()
    {
        if (photonc_text.activeSelf && PhotonNetwork.IsConnectedAndReady == true)
        {
            //photonc_text.SetActive(false);
            //connected_text.SetActive(true);
        }

        if (PhotonNetwork.PlayerList.Length == 2 && runOnce == false)
        {
            runOnce = true;
            //connected_text.SetActive(false);
            //connection_p2_text.SetActive(true);
            //Invoke("changeText", 2f);
        }


        if (controlbool == true)
        {
            controlShow.SetActive(true);
        }

    }

    [PunRPC]
    public void moveMe(Vector2 vector)
    {

        if (controlbool == true)
        {
            return;
        }

        //Debug.Log("before: " + p1index);
        if (vector.x < 0 && vector.x >= -1)
        {
            if (p1index > 0)
            {
                p1index -= .5f;
            }

        }
        else if (vector.x <= 1 && vector.x > 0)
        {
            if (p1index < 2)
            {
                p1index += .5f;
            }

        }

        //Debug.Log("after: " + p1index);

        if (p1index == 0)
        {

            p1_none.SetActive(false);
            p1_Q.SetActive(false);

            ml_sel.SetActive(true);
            ml_on = true;
            p1_ML.SetActive(true);
            if (p2index == 2)
            {
                continueIcon.SetActive(true);
            }

        }
        else if (p1index == 1)
        {
            continueIcon.SetActive(false);
            ml_sel.SetActive(false);
            q_sel.SetActive(false);
            p1_ML.SetActive(false);
            p1_Q.SetActive(false);

            if (p2index == 0)
            {
                ml_sel.SetActive(true);
                q_on = false;
            }
            else if (p2index == 1)
            {
                ml_on = false;
                q_on = false;
            }
            else if (p2index == 2)
            {
                q_sel.SetActive(true);
                ml_on = false;
            }


            p1_none.SetActive(true);

        }
        else if (p1index == 2)
        {

            p1_none.SetActive(false);
            p1_ML.SetActive(false);

            q_sel.SetActive(true);
            q_on = true;
            p1_Q.SetActive(true);

                if (p2index == 0)
            {
                continueIcon.SetActive(true);
            }
        }

    }

    [PunRPC]
    public void moveMe2(Vector2 vector)
    {

        if (controlbool == true)
        {
            return;
        }

        //Debug.Log("before: " + p2index);
        if (vector.x < 0 && vector.x >= -1)
        {
            if (p2index > 0)
            {
                p2index -= .5f;
            }

        }
        else if (vector.x <= 1 && vector.x > 0)
        {
            if (p2index < 2)
            {
                p2index += .5f;
            }

        }

        //Debug.Log("after: " + p2index);

        if (p2index == 0)
        {
                if (p1index == 2)
                {
                    continueIcon.SetActive(true);
                }
            p2_none.SetActive(false);
            p2_Q.SetActive(false);

            ml_sel.SetActive(true);
            ml_on = true;
            p2_ML.SetActive(true);

        }
        else if (p2index == 1)
        {
            continueIcon.SetActive(false);
            ml_sel.SetActive(false);
            q_sel.SetActive(false);
            p2_ML.SetActive(false);
            p2_Q.SetActive(false);

            if (p1index == 0)
            {
                ml_sel.SetActive(true);
                q_on = false;
            }
            else if (p1index == 1)
            {
                ml_on = false;
                q_on = false;
            }
            else if (p1index == 2)
            {
                q_sel.SetActive(true);
                ml_on = false;
            }




            p2_none.SetActive(true);

        }
        else if (p2index == 2)
        {

            p2_none.SetActive(false);
            p2_ML.SetActive(false);

            q_sel.SetActive(true);
            q_on = true;
            p2_Q.SetActive(true);

            if (p1index == 0)
            {
                continueIcon.SetActive(true);
            }
        }

    }


    [PunRPC]
    public void charSelect()
    {



        if (ml_on && q_on)
        {
            controlbool = true;
            Debug.Log("Success");
        }
        else
        {
            //Error Seound

        }
    }

    private void changeScene()
    {
        //SceneManager.LoadScene("Scroller_1_1");
        loadVar = true;
    }

    private void showControls()
    {
        Canvas mcanvas = blur.GetComponent<Canvas>();
        mcanvas.sortingOrder = 4;
        controls.SetActive(true);
    }

    private void changeText()
    {
        //connection_p2_text.SetActive(false);
        //photon_controls.SetActive(true);
    }
}
