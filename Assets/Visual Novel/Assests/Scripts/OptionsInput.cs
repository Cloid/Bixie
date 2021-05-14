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
    public GameObject controls;

    public GameObject EventSystem;

    public Selectable Music_Slider;
    public Selectable tmpNull;

    public Selectable Sound_Slider;
    public GameObject OptionsUI;
    public GameObject Credits_1;
    public GameObject Credits_2;
    public GameObject parentScene;
    public GameObject controls_txt;
    public Text MusicNum;
    public Text SoundNum;
    public AudioOptions AudOptions;
    public DestroyMe Play;
    public Text cornerText;

    public int sceneNum;

    public bool inCredits = false;

    public bool inControls = false;

    private void Start()
    {
        Play = parentScene.GetComponent<DestroyMe>();
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

        if (inCredits)
        {
            Keyboard kb = InputSystem.GetDevice<Keyboard>();
            Gamepad gp = InputSystem.GetDevice<Gamepad>();

            if (Credits_2.activeSelf)
            {
                if (gp == null)
                {
                    if (kb.enterKey.wasPressedThisFrame)
                    {
                        Credits_1.SetActive(false);
                        Credits_2.SetActive(false);
                        OptionsUI.SetActive(true);
                        inCredits = false;
                    }
                }
                else
                {
                    if (kb.enterKey.wasPressedThisFrame || gp.aButton.wasPressedThisFrame || gp.buttonEast.wasPressedThisFrame)
                    {
                        Credits_1.SetActive(false);
                        Credits_2.SetActive(false);
                        OptionsUI.SetActive(true);
                        inCredits = false;
                    }
                }
            }
            else
            {
                if (gp == null)
                {
                    if (kb.enterKey.wasPressedThisFrame)
                    {
                        Credits_1.SetActive(false);
                        Credits_2.SetActive(true);
                    }
                }
                else
                {
                    if (kb.enterKey.wasPressedThisFrame || gp.aButton.wasPressedThisFrame || gp.buttonEast.wasPressedThisFrame)
                    {
                        Credits_1.SetActive(false);
                        Credits_2.SetActive(true);
                    }
                }
            }
        }
        else if (inControls)
        {
            Keyboard kb = InputSystem.GetDevice<Keyboard>();
            Gamepad gp = InputSystem.GetDevice<Gamepad>();
            
            if (gp == null)
            {
                if (kb.enterKey.wasPressedThisFrame)
                {
                    controls.SetActive(false);
                    OptionsUI.SetActive(true);
                    inControls = false;
                }
            }
            else
            {
                if (kb.enterKey.wasPressedThisFrame || gp.aButton.wasPressedThisFrame || gp.buttonEast.wasPressedThisFrame)
                {
                    controls.SetActive(false);
                    OptionsUI.SetActive(true);
                    inControls = false;
                }
            }
        }
        else
        {

            Keyboard kb = InputSystem.GetDevice<Keyboard>();
            Gamepad gp = InputSystem.GetDevice<Gamepad>();
            if (gp == null)
            {
                if (kb.wKey.wasPressedThisFrame || kb.upArrowKey.wasPressedThisFrame)
                {
                    if (tracker > 0 && tracker <= 2)
                    {
                        tracker--;
                    }
                    else if (tracker > 2)
                    {
                        tracker = 1;
                    }
                }
                else if (kb.sKey.wasPressedThisFrame || kb.downArrowKey.wasPressedThisFrame)
                {
                    if (tracker >= 0 && tracker < 2)
                    {
                        tracker++;
                    }
                }
                else if (kb.aKey.wasPressedThisFrame || kb.leftArrowKey.wasPressedThisFrame)
                {
                    if (tracker > 2 && tracker <= 4)
                    {
                        tracker--;
                    }
                }
                else if (kb.dKey.wasPressedThisFrame || kb.rightArrowKey.wasPressedThisFrame)
                {
                    if (tracker >= 2 && tracker < 4)
                    {
                        tracker++;
                    }
                }
            }

            else
            {
                if (kb.wKey.wasPressedThisFrame || kb.upArrowKey.wasPressedThisFrame || gp.leftStick.up.wasPressedThisFrame)
                {
                    if (tracker > 0 && tracker <= 2)
                    {
                        tracker--;
                    }
                    else if (tracker > 2)
                    {
                        tracker = 1;
                    }
                }
                else if (kb.sKey.wasPressedThisFrame || kb.downArrowKey.wasPressedThisFrame || gp.leftStick.down.wasPressedThisFrame)
                {
                    if (tracker >= 0 && tracker < 2)
                    {
                        tracker++;
                    }
                }
                else if (kb.aKey.wasPressedThisFrame || kb.leftArrowKey.wasPressedThisFrame || gp.leftStick.left.wasPressedThisFrame)
                {
                    if (tracker > 2 && tracker <= 4)
                    {
                        tracker--;
                    }
                }
                else if (kb.dKey.wasPressedThisFrame || kb.rightArrowKey.wasPressedThisFrame || gp.leftStick.right.wasPressedThisFrame)
                {
                    if (tracker >= 2 && tracker < 4)
                    {
                        tracker++;
                    }
                }
            }


            Color colDeselected = new Color(0.270588f, 0.454902f, 0.525490f, 1.0f);
            Color colSelected   = new Color(1.0f, 0.109804f, 0.0f, 1.0f);

            if (tracker == 0)
            {
                Music.GetComponent<Text>().color = colSelected;
                Sound.GetComponent<Text>().color = colDeselected;
                Credits.GetComponent<Text>().color = colDeselected;
                Txt.GetComponent<Text>().color = colDeselected;
                controls_txt.GetComponent<Text>().color = colDeselected;
                //EventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(Music_Slider);
                Music_Slider.Select();
            }
            else if (tracker == 1)
            {
                Music.GetComponent<Text>().color = colDeselected;
                Sound.GetComponent<Text>().color = colSelected;
                Credits.GetComponent<Text>().color = colDeselected;
                Txt.GetComponent<Text>().color = colDeselected;
                controls_txt.GetComponent<Text>().color = colDeselected;
                Sound_Slider.Select();
            }
            else if (tracker == 2)
            {
                Music.GetComponent<Text>().color = colDeselected;
                Sound.GetComponent<Text>().color = colDeselected;
                Txt.GetComponent<Text>().color = colSelected;
                Credits.GetComponent<Text>().color = colDeselected;
                controls_txt.GetComponent<Text>().color = colDeselected;
                //EventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
                tmpNull.Select();

                if (gp == null)
                {
                    if (kb.enterKey.wasPressedThisFrame)
                    {
                        // if (sceneNum == 0)
                        // {
                        //     SceneManager.LoadScene("Title");
                        // }
                        // else
                        // {
                        //     SceneManager.LoadScene(sceneNum);
                        // }
                        Play.DestroyScene();
                    }
                }
                else
                {
                    if (kb.enterKey.wasPressedThisFrame || gp.aButton.wasPressedThisFrame || gp.buttonEast.wasPressedThisFrame)
                    {
                        // if (sceneNum == 0)
                        // {
                        //     SceneManager.LoadScene("Title");
                        // }
                        // else
                        // {
                        //     SceneManager.LoadScene(sceneNum);
                        // }

                        Play.DestroyScene();


                    }
                }


            }
            else if (tracker == 3)
            {
                Music.GetComponent<Text>().color = colDeselected;
                Sound.GetComponent<Text>().color = colDeselected;
                Credits.GetComponent<Text>().color = colSelected;
                Txt.GetComponent<Text>().color = colDeselected;
                controls_txt.GetComponent<Text>().color = colDeselected;
                //EventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
                tmpNull.Select();
                if (gp == null)
                {
                    if (kb.enterKey.wasPressedThisFrame)
                    {
                        OptionsUI.SetActive(false);
                        Credits_1.SetActive(true);
                        inCredits = true;
                    }
                }
                else
                {
                    if (kb.enterKey.wasPressedThisFrame || gp.aButton.wasPressedThisFrame || gp.buttonEast.wasPressedThisFrame)
                    {
                        OptionsUI.SetActive(false);
                        Credits_1.SetActive(true);
                        inCredits = true;
                    }
                }



            }
            else if (tracker == 4)
            {
                Music.GetComponent<Text>().color = colDeselected;
                Sound.GetComponent<Text>().color = colDeselected;
                Credits.GetComponent<Text>().color = colDeselected;
                Txt.GetComponent<Text>().color = colDeselected;
                controls_txt.GetComponent<Text>().color = colSelected;
                tmpNull.Select();

                if (gp == null)
                {
                    if (kb.enterKey.wasPressedThisFrame)
                    {
                        OptionsUI.SetActive(false);
                        controls.SetActive(true);
                        inControls = true;
                    }
                }
                else
                {
                    if (kb.enterKey.wasPressedThisFrame || gp.aButton.wasPressedThisFrame || gp.buttonEast.wasPressedThisFrame)
                    {
                        OptionsUI.SetActive(false);
                        controls.SetActive(true);
                        inControls = true;
                    }
                }
            }

        }
    }
}
