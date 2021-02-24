using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class OptionsInput : MonoBehaviour
{
    public InputAction wasd;
    public int tracker = 0;

    public Text Music;
    public Text Sound;
    public Text Credits;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnEnable() {
        wasd.Enable();
    }

    void OnDisable() {
        wasd.Disable();
    }

    // Update is called once per frame
    void Update()
    {

        Keyboard kb = InputSystem.GetDevice<Keyboard>();
        Gamepad gp = InputSystem.GetDevice<Gamepad>();
        if(kb.wKey.wasPressedThisFrame || gp.leftStick.up.wasPressedThisFrame){
            Debug.Log("Going up!");
            if(tracker > 0 && tracker <= 2){
                tracker--;
                Debug.Log("Tracker: " + tracker);
            }
        } else if(kb.sKey.wasPressedThisFrame || gp.leftStick.down.wasPressedThisFrame){
            if(tracker >= 0 && tracker < 2){
                tracker++;
                Debug.Log("Tracker: " + tracker);
            }
        }

        if(tracker == 0){
            
        } else if(tracker ==1){

        } else if(tracker == 2){

        }

    }
}
