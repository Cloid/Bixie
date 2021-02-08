using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TimeController : MonoBehaviour
{
    public QinyangControls controls;
    public GameObject Qinyang;
    public GameObject Mei;
     void PauseGame ()
    {
        //Time.timeScale = 0f;
        //controls.Disable();
        Qinyang.GetComponent<Player>().enabled = false;
        Mei.GetComponent<Player2>().enabled = false;
        Debug.Log("Called Pause");
    }

 void ResumeGame ()
    {
        Qinyang.GetComponent<Player>().enabled = true;
        Mei.GetComponent<Player2>().enabled = true;
        //controls.Enable();
        Debug.Log("Called Resume");
    }
}
