using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobTimer : MonoBehaviour
{
    public Photon_NextLevel photon_NextLevel;
    float timeLeft = 60.0f;

    void Update()
     {
         timeLeft -= Time.deltaTime;
         if(timeLeft < 0)
         {
             Debug.Log("TP");
             photon_NextLevel.enabled = true;
             //nextLevel.SetActive(true);
             //GameOver();
         }
     }

}
