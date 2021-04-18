using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using Fungus;

public class AudioOptions : MonoBehaviour
{
    //public InputAction Pause;
    FMOD.Studio.EventInstance SFXVolumeTestEvent;

    FMOD.Studio.Bus Music;
    FMOD.Studio.Bus SFX;
    public float MusicVolume = 0.5f;
    public float SFXVolume = 0.5f;
    public float MasterVolume = 1f;

    public float sfxVolSave = 0f;
    public int sceneNum = 0;


    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        Music = FMODUnity.RuntimeManager.GetBus("bus:/Music");
        SFX = FMODUnity.RuntimeManager.GetBus("bus:/SFX");
        sfxVolSave = SFXVolume;

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
        var FungusAudio = FindObjectsOfType<WriterAudio>(true);
        //Debug.Log(FungusAudio.Length);
        for(int idx = 0; idx <= FungusAudio.Length-1; idx++){
            WriterAudio aud = FungusAudio[idx].GetComponent<WriterAudio>();
            aud.setVolume(sfxVolSave);
        }

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
        sfxVolSave = newSFXVolume;

        FMOD.Studio.PLAYBACK_STATE PbState;
        SFXVolumeTestEvent.getPlaybackState(out PbState);
        if (PbState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            SFXVolumeTestEvent.start();
        }
        
        var FungusAudio = FindObjectsOfType<WriterAudio>(true);
        Debug.Log(FungusAudio.Length);
        for(int idx = 0; idx <= FungusAudio.Length-1; idx++){
            WriterAudio aud = FungusAudio[idx].GetComponent<WriterAudio>();
            aud.setVolume(newSFXVolume);
        }

    }
}
