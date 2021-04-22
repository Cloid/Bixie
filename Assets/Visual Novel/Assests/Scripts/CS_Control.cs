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
    private bool runOnce = false;
    // Start is called before the first frame update
    void Start()
    {
        PhotonProxy_get = GameObject.Find("AudioController");
        photonGetter = PhotonProxy_get.GetComponent<PhotonGetter>();

        if(photonGetter.local==false){
            ctext.SetActive(false);
            photonc_text.SetActive(true);
            photonView = GetComponent<PhotonView>();
            //photonView.TransferOwnership(1);
        }

        goCam = gameObject.GetComponent<CameraShake>();
    }

    private void Update() {
        if(photonc_text.activeSelf && PhotonNetwork.IsConnectedAndReady == true){
            photonc_text.SetActive(false);
            connected_text.SetActive(true);
        }

        if(PhotonNetwork.PlayerList.Length==2 && runOnce == false){
            runOnce = true;
            connected_text.SetActive(false);
            connection_p2_text.SetActive(true);
            Invoke("changeText", 2f);
        }

        if(PhotonNetwork.IsMasterClient && loadVar){
            loadObject.SetActive(true);
        }

    }

    [PunRPC]
    public void moveMe(Vector2 vector)
    {
        Debug.Log("Test");
        if(connected_text.activeSelf){
            return;
        }

        Debug.Log(vector);
        if (!p1_selected)
        {
            if (vector.x < 0 && vector.x >= -1 && (!ml_on))
            {
                blur.SetActive(true);
                q_sel.SetActive(false);
                //ml_unsel.SetActive(false);
                ml_sel.SetActive(true);
                ml_on = true;
                q_on = false;
            }
            else if (vector.x <= 1 && vector.x > 0 && (!q_on))
            {
                blur.SetActive(true);
                ml_sel.SetActive(false);
                //q_unsel.SetActive(false);
                q_sel.SetActive(true);
                ml_on = false;
                q_on = true;
            }
        }
    }

    [PunRPC]
    public void charSelect()
    {
        if(connected_text.activeSelf){
            return;
        }

        if(controls.activeSelf == true){
            Debug.Log("test");
            sceneTransition.SetActive(true);
            //Invoke("changeScene", 1f);
            if(PhotonNetwork.IsMasterClient){
                changeScene();
            }
        }
        
        if (q_sel.activeSelf == true && !(p1_selected))
        {
            myObj = GameObject.FindGameObjectsWithTag("PlayerInput");
            p1 = myObj[0].GetComponent<PlayerInputHandler>();
            if (myObj.Length == 1 && PhotonNetwork.OfflineMode)
            {
                ctext.SetActive(false);
                p2_text.SetActive(true);
            }
            else if(!PhotonNetwork.OfflineMode){
                ctext.SetActive(false);
                p2_text.SetActive(false);
                photon_controls.SetActive(false);

                p1_selected = true;
                ml_sel.SetActive(true);
                goCam.ShakeIt();
                Invoke("showControls", 1);

            }
            else
            {
                ctext.SetActive(false);
                p2_text.SetActive(false);
                photon_controls.SetActive(false);

                p2 = myObj[1].GetComponent<PlayerInputHandler>();
                p1_selected = true;
                ml_sel.SetActive(true);
                goCam.ShakeIt();
                Invoke("showControls", 1);
            }
        }
        else if (!p1_selected)
        {
            myObj = GameObject.FindGameObjectsWithTag("PlayerInput");
            p1 = myObj[0].GetComponent<PlayerInputHandler>();
            //Debug.Log(p2);
            if (myObj.Length == 1)
            {
                ctext.SetActive(false);
                p2_text.SetActive(true);
            }
            else if(!PhotonNetwork.OfflineMode){

                ctext.SetActive(false);
                p2_text.SetActive(false);
                photon_controls.SetActive(false);

                if(PhotonNetwork.IsMasterClient){
                    p1.index = 1;
                } else{
                    p1.index = 0;
                }
                p1_selected = true;
                q_sel.SetActive(true);
                goCam.ShakeIt();
                Invoke("showControls", 1);

            }
            else
            {
                ctext.SetActive(false);
                p2_text.SetActive(false);
                photon_controls.SetActive(false);

                p2 = myObj[1].GetComponent<PlayerInputHandler>();
                p1_selected = true;
                p1.index = 1;
                p2.index = 0;
                q_sel.SetActive(true);
                goCam.ShakeIt();
                Invoke("showControls", 1);
            }
        }
    }

    private void changeScene(){
        //SceneManager.LoadScene("Scroller_1_1");
        loadVar = true;
    }

    private void showControls(){
        Canvas mcanvas = blur.GetComponent<Canvas>();
        mcanvas.sortingOrder = 4;
        controls.SetActive(true);
    }

    private void changeText(){
        connection_p2_text.SetActive(false);
        photon_controls.SetActive(true);
    }
}
