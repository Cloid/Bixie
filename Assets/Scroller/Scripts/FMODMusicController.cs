﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODMusicController : MonoBehaviour {

    // adjustable variables
    public string songEvent;
    public int intensity;

    // event insttance variable for fmod
    private static FMOD.Studio.EventInstance music;
    private int enemiesCount;

    void Start()
    {
        music = FMODUnity.RuntimeManager.CreateInstance(songEvent);

		music.start();
        music.release();
        
        // directly set music to fight
        // need to implement auto music transition later
        //SetIntensity(intensity);
    }

    void Update()
    {
        enemiesCount = FindObjectsOfType<Enemy>().Length;
        if (enemiesCount > 0)
        {
            SetIntensity(1);
        }
        else
        {
            SetIntensity(0);
        }
    }

    public void SetIntensity(int i)
    {
        //music.setParameterByName("Intensity", i);
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Intensity", i);

        intensity = i;
    }

    private void OnDestroy()
    {
        music.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

}
