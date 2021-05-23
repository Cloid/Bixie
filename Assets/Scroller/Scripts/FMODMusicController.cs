using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODMusicController : MonoBehaviour {

    // adjustable variables
    public string songEvent;
    public float delay_time;

    // event insttance variable for fmod
    private static FMOD.Studio.EventInstance music;
    private int intensity;
    private int enemiesCount;
    private float count_down;

    void Start()
    {
        music = FMODUnity.RuntimeManager.CreateInstance(songEvent);

		music.start();
        music.release();
        
        // directly set music to fight
        // need to implement auto music transition later
        // SetIntensity(intensity);
    }

    void Update()
    {
        enemiesCount = FindObjectsOfType<Enemy>().Length;
        if (enemiesCount > 0)
        {
            SetIntensity(1);
            count_down = delay_time;
        }
        else
        {
            // countdown before switching back to peace
            if (count_down > 0)
            {
                count_down -= Time.deltaTime;
            }
            else
            {
                SetIntensity(0);
            }
        }
    }

    public void SetIntensity(int i)
    {
        if (i != intensity)
        {
            //music.setParameterByName("Intensity", i);
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Intensity", i);

            intensity = i;
        }
    }

    private void OnDestroy()
    {
        music.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

}
