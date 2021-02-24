﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class OptionsInput : MonoBehaviour
{
    public InputAction wasd;
    public int tracker = 0;

    public GameObject Music;
    public GameObject Sound;
    public GameObject Credits;

    public GameObject EventSystem;

    public GameObject Music_Slider;

    public GameObject Sound_Slider;


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

        Keyboard kb = InputSystem.GetDevice<Keyboard>();
        Gamepad gp = InputSystem.GetDevice<Gamepad>();
        if (gp == null)
        {
            if (kb.wKey.wasPressedThisFrame)
            {
                if (tracker > 0 && tracker <= 2)
                {
                    tracker--;
                }
            }
            else if (kb.sKey.wasPressedThisFrame)
            {
                if (tracker >= 0 && tracker < 2)
                {
                    tracker++;
                }
            }
        }
        else
        {
            if (kb.wKey.wasPressedThisFrame || gp.leftStick.up.wasPressedThisFrame)
            {
                Debug.Log("Going up!");
                if (tracker > 0 && tracker <= 2)
                {
                    tracker--;
                }
            }
            else if (kb.sKey.wasPressedThisFrame || gp.leftStick.down.wasPressedThisFrame)
            {
                if (tracker >= 0 && tracker < 2)
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
            EventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(Music_Slider);
        }
        else if (tracker == 1)
        {
            Music.GetComponent<Text>().color = Color.black;
            Sound.GetComponent<Text>().color = Color.red;
            Credits.GetComponent<Text>().color = Color.black;
            EventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(Sound_Slider);
        }
        else if (tracker == 2)
        {
            Music.GetComponent<Text>().color = Color.black;
            Sound.GetComponent<Text>().color = Color.black;
            Credits.GetComponent<Text>().color = Color.red;
            EventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
        }

    }
}