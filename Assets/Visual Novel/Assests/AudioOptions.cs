using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioOptions : MonoBehaviour
{
    FMOD.Studio.EventInstance SFXVolumeTestEvent;

     FMOD.Studio.Bus Music;
     FMOD.Studio.Bus SFX;
     float MusicVolume = 0.5f;
     float SFXVolume = 0.5f;
     float MasterVolume = 1f;

     public int sceneNum = 0;

     void Awake ()
     {
        DontDestroyOnLoad(this.gameObject);
        Music = FMODUnity.RuntimeManager.GetBus ("bus:/Music");
        SFX = FMODUnity.RuntimeManager.GetBus ("bus:/SFX");
        //SFXVolumeTestEvent = FMODUnity.RuntimeManager.CreateInstance ("event:/SFX/SFXVolumeTest");
     }

     void Update () 
     {
          Music.setVolume (MusicVolume);
          SFX.setVolume (SFXVolume);
          Debug.Log(sceneNum);
     }


    public void incNum(){
        sceneNum = SceneManager.GetActiveScene().buildIndex;
    }
     public void MasterVolumeLevel (float newMasterVolume)
     {
          MasterVolume = newMasterVolume;
     }

     public void MusicVolumeLevel (float newMusicVolume)
     {
          MusicVolume = newMusicVolume;
     }

     public void SFXVolumeLevel (float newSFXVolume)
     {
          SFXVolume = newSFXVolume;

          FMOD.Studio.PLAYBACK_STATE PbState;
          SFXVolumeTestEvent.getPlaybackState (out PbState);
          if (PbState != FMOD.Studio.PLAYBACK_STATE.PLAYING) 
          {
               SFXVolumeTestEvent.start ();
          }
     }
}
