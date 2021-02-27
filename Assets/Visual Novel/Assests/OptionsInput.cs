using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class OptionsInput : MonoBehaviour
{
    public InputAction wasd;
    public int tracker = 0;

    public GameObject Music;
    public GameObject Sound;
    public GameObject Credits;
    public GameObject Txt;

    public GameObject EventSystem;

    public GameObject Music_Slider;

    public GameObject Sound_Slider;
    public GameObject AudioOptions;
    public Text cornerText;

    public int sceneNum;

    private void Start() {
        AudioOptions = GameObject.Find("AudioController");
    }
    void OnEnable()
    {
        wasd.Enable();
    }

    void OnDisable()
    {
        wasd.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if(AudioOptions == null){
            AudioOptions = GameObject.Find("AudioController");  
        } else{
            sceneNum = AudioOptions.GetComponent<AudioOptions>().sceneNum;
            Debug.Log(sceneNum);
        }

        if(sceneNum==0){
            cornerText.text = "Title";
        } else {
            cornerText.text = "Back";
        }

        Keyboard kb = InputSystem.GetDevice<Keyboard>();
        Gamepad gp = InputSystem.GetDevice<Gamepad>();
        if (gp == null)
        {
            if (kb.wKey.wasPressedThisFrame)
            {
                if (tracker > 0 && tracker <= 3)
                {
                    tracker--;
                }
            }
            else if (kb.sKey.wasPressedThisFrame)
            {
                if (tracker >= 0 && tracker < 3)
                {
                    tracker++;
                }
            }
        }
        else
        {
            if (kb.wKey.wasPressedThisFrame || gp.leftStick.up.wasPressedThisFrame)
            {
                if (tracker > 0 && tracker <= 3)
                {
                    tracker--;
                }
            }
            else if (kb.sKey.wasPressedThisFrame || gp.leftStick.down.wasPressedThisFrame)
            {
                if (tracker >= 0 && tracker < 3)
                {
                    tracker++;
                }
            }
        }



        if (tracker == 0)
        {
            Music.GetComponent<Text>().color = Color.red;
            Sound.GetComponent<Text>().color = Color.black;
            Credits.GetComponent<Text>().color = Color.black;
            Txt.GetComponent<Text>().color = Color.black;
            EventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(Music_Slider);
        }
        else if (tracker == 1)
        {
            Music.GetComponent<Text>().color = Color.black;
            Sound.GetComponent<Text>().color = Color.red;
            Credits.GetComponent<Text>().color = Color.black;
            Txt.GetComponent<Text>().color = Color.black;
            EventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(Sound_Slider);
        }
        else if (tracker == 2)
        {
            Music.GetComponent<Text>().color = Color.black;
            Sound.GetComponent<Text>().color = Color.black;
            Txt.GetComponent<Text>().color = Color.red;
            Credits.GetComponent<Text>().color = Color.black;
            EventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
            if(gp==null){
                if(kb.enterKey.wasPressedThisFrame){
                    if(sceneNum==0){
                        SceneManager.LoadScene("Title");
                    } else {
                        SceneManager.LoadScene(sceneNum);
                    }
                }
            } else {
                if(kb.enterKey.wasPressedThisFrame || gp.aButton.wasPressedThisFrame){
                    if(sceneNum==0){
                        SceneManager.LoadScene("Title");
                    } else {
                        SceneManager.LoadScene(sceneNum);
                    }
                }
            }
            

        } 
        else if (tracker == 3)
        {
            Music.GetComponent<Text>().color = Color.black;
            Sound.GetComponent<Text>().color = Color.black;
            Credits.GetComponent<Text>().color = Color.red;
            Txt.GetComponent<Text>().color = Color.black;
            EventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
            if(gp==null){
                if(kb.enterKey.wasPressedThisFrame){
                SceneManager.LoadScene("Title");
                }
            } else {
                if(kb.enterKey.wasPressedThisFrame || gp.aButton.wasPressedThisFrame){
                SceneManager.LoadScene("Title");
                }
            }
        }


    }
}
