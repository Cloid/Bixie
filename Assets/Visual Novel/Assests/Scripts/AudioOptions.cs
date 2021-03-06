using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class AudioOptions : MonoBehaviour
{
    //public InputAction Pause;
    FMOD.Studio.EventInstance SFXVolumeTestEvent;

    FMOD.Studio.Bus Music;
    FMOD.Studio.Bus SFX;
    public float MusicVolume = 0.5f;
    public float SFXVolume = 0.5f;
    public float MasterVolume = 1f;

    public int sceneNum = 0;

    private AudioSource Narr_vol;
    private AudioSource Char_vol;
    private AudioSource Real_vol;


    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        Music = FMODUnity.RuntimeManager.GetBus("bus:/Music");
        SFX = FMODUnity.RuntimeManager.GetBus("bus:/SFX");

        //SFXVolumeTestEvent = FMODUnity.RuntimeManager.CreateInstance ("event:/SFX/SFXVolumeTest");
    }

    void Update()
    {
        // Implementation for future Options anywhere
        /*
        Keyboard kb = InputSystem.GetDevice<Keyboard>();
        Gamepad gp = InputSystem.GetDevice<Gamepad>();

        if(gp==null){
            if(kb.escapeKey.wasPressedThisFrame){
                Time.timeScale = 0;
                Application.LoadLevelAdditive("Options");
            }
        }*/

        Music.setVolume(MusicVolume);
        SFX.setVolume(SFXVolume);
        
    }


    public void incNum()
    {
        sceneNum = SceneManager.GetActiveScene().buildIndex;
    }
    public void MasterVolumeLevel(float newMasterVolume)
    {
        MasterVolume = newMasterVolume;
    }

    public void MusicVolumeLevel(float newMusicVolume)
    {
        MusicVolume = newMusicVolume;
    }

    public void SFXVolumeLevel(float newSFXVolume)
    {
        SFXVolume = newSFXVolume;

        FMOD.Studio.PLAYBACK_STATE PbState;
        SFXVolumeTestEvent.getPlaybackState(out PbState);
        if (PbState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            SFXVolumeTestEvent.start();
        }

/*
        if(Narr_vol == null){
            Narr_vol = GameObject.Find("NarrativeDialog").GetComponent<AudioSource>();
            Narr_vol.volume = newSFXVolume;
            Debug.Log("Finished NArr");
        }

        if(Char_vol == null){
            Char_vol = GameObject.Find("SayDialog").GetComponent<AudioSource>();            
            Char_vol.volume = newSFXVolume;
            Debug.Log("Finished char");

        }

        if(Real_vol == null){
            Real_vol = GameObject.Find("SayDialog").GetComponent<AudioSource>();           
            Real_vol.volume = newSFXVolume;
            Debug.Log("Finished real");
        }*/
    }
}
