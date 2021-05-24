using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColors_Scroller : MonoBehaviour
{
    private GameObject Q_neutral;
    private GameObject ML_neutral;

    // Start is called before the first frame update
    public void FindQ(){
        Q_neutral = GameObject.Find("icon_Q_neutral");
        Debug.Log(Q_neutral);
    }

    public void FindML(){
        ML_neutral = GameObject.Find("icon_ML_neutral");
        Debug.Log(ML_neutral);
    }

    public void muteQ(){
        Q_neutral.GetComponent<Image>().color = new Color32(128,128,128,255);
    }
    public void colorQ(){
        Q_neutral.GetComponent<Image>().color = new Color32(255,255,255,255);
    }
    public void muteML(){
        ML_neutral.GetComponent<Image>().color = new Color32(128,128,128,255);
    }
    public void colorML(){
        ML_neutral.GetComponent<Image>().color = new Color32(255,255,255,255);
    }

}
