using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioProxy : MonoBehaviour
{
    public float MusicVol = 0.5f;
    public float SFXVol = 0.5f;
    
    public AudioOptions Aud;
    // Start is called before the first frame update
    void Start()
    {
        Aud = GameObject.Find("AudioController").GetComponent<AudioOptions>();
        MusicVol = Aud.MusicVolume;
        SFXVol = Aud.SFXVolume;
    }

    public void MusicProxy(float newVol){
        MusicVol = newVol;
        Aud.MusicVolumeLevel(MusicVol);
    }

    public void SFXProxy(float newVol){
        SFXVol = newVol;
        Aud.SFXVolumeLevel(SFXVol);
    }
}
