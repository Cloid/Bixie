using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODSounds : MonoBehaviour
{
    public void PlayHoverSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Sounds/UI_click");
    }

    public void PlaySelectSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Sounds/UI_select");
    }
}
