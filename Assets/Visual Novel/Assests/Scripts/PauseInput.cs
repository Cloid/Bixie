﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Fungus;
using UnityEngine.SceneManagement;
public class PauseInput : MonoBehaviour
{
    public Flowchart Pause;
    public Flowchart Intro;
    private bool inOptions = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        Keyboard kb = InputSystem.GetDevice<Keyboard>();
        Gamepad gp = InputSystem.GetDevice<Gamepad>();

        if (!inOptions)
        {
            if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                if (gp == null)
                {
                    if (kb.escapeKey.wasPressedThisFrame)
                    {
                        Time.timeScale = 0;
                        //PauseMenu.SetActive(true);
                        inOptions = true;
                        Pause.ExecuteBlock("Pause");
                        //Application.LoadLevelAdditive("Options");
                    }
                }
                else
                {
                    if (kb.escapeKey.wasPressedThisFrame || gp.startButton.wasPressedThisFrame)
                    {
                        Time.timeScale = 0;
                        //PauseMenu.SetActive(true);
                        inOptions = true;
                        Pause.ExecuteBlock("Pause");
                        //Application.LoadLevelAdditive("Options");
                    }
                }
            }
            else if (SceneManager.GetActiveScene().buildIndex == 4)
            {
                if (!inOptions)
                {
                    if (gp == null)
                    {
                        if (kb.escapeKey.wasPressedThisFrame)
                        {
                            Time.timeScale = 0;
                            //PauseMenu.SetActive(true);
                            inOptions = true;
                            Pause.ExecuteBlock("Pause_nonVN");
                            //Application.LoadLevelAdditive("Options");
                        }
                    }
                    else
                    {
                        if (kb.escapeKey.wasPressedThisFrame || gp.startButton.wasPressedThisFrame)
                        {
                            Time.timeScale = 0;
                            //PauseMenu.SetActive(true);
                            inOptions = true;
                            Pause.ExecuteBlock("Pause_nonVN");
                            //Application.LoadLevelAdditive("Options");
                        }
                    }
                }
            }

        }



    }

    public void ResumeScene()
    {
        Time.timeScale = 1;
        var FungusPause = FindObjectsOfType<DialogInput>();
        Debug.Log(FungusPause);
        for(int idx = 0; idx <= FungusPause.Length-1; idx++){
            DialogInput dig = FungusPause[idx].GetComponent<DialogInput>();
            dig.setPause();
        }
        //Destroy(gameObject);
        //gameObject.SetActive(false);
    }

    public void ResetBool()
    {
        inOptions = false;
    }

    public void OptionScene()
    {
        Application.LoadLevelAdditive("Options");
    }
    public void IntroScene()
    {
        Intro.ExecuteBlock("TitleList");
    }

}
