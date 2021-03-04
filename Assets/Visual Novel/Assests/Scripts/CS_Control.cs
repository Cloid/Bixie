using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    private PlayerInputHandler p1;
    private PlayerInputHandler p2;
    private CameraShake goCam;
    private bool ml_on = false;
    private bool q_on = false;
    public bool p1_selected = false;
    // Start is called before the first frame update
    void Start()
    {
        goCam = gameObject.GetComponent<CameraShake>();
    }

    public void moveMe(Vector2 vector)
    {
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

    public void charSelect()
    {
        if (q_sel.activeSelf == true && !(p1_selected))
        {
            myObj = GameObject.FindGameObjectsWithTag("PlayerInput");
            p1 = myObj[0].GetComponent<PlayerInputHandler>();
            if (myObj.Length == 1)
            {
                ctext.SetActive(false);
                p2_text.SetActive(true);
            }
            else
            {
                ctext.SetActive(false);
                p2_text.SetActive(false);
                p2 = myObj[1].GetComponent<PlayerInputHandler>();
                p1_selected = true;
                ml_sel.SetActive(true);
                goCam.ShakeIt();
                Invoke("changeScene", 1);
            }
        }
        else if (!p1_selected)
        {
            myObj = GameObject.FindGameObjectsWithTag("PlayerInput");
            p1 = myObj[0].GetComponent<PlayerInputHandler>();
            Debug.Log(p2);
            if (myObj.Length == 1)
            {
                ctext.SetActive(false);
                p2_text.SetActive(true);
            }
            else
            {
                ctext.SetActive(false);
                p2_text.SetActive(false);
                p2 = myObj[1].GetComponent<PlayerInputHandler>();
                p1_selected = true;
                p1.index = 1;
                p2.index = 0;
                q_sel.SetActive(true);
                goCam.ShakeIt();
                Invoke("changeScene", 1);
            }
        }
    }

    private void changeScene(){
        SceneManager.LoadScene("Scroller_1_1");
    }
}
