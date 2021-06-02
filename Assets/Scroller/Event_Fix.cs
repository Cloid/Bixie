using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Fix : MonoBehaviour
{
    public void inputMe(){
        MonoBehaviour[] comps = GetComponents<MonoBehaviour>();
        foreach(MonoBehaviour c in comps){
            c.enabled = true;
        }
        //EventSystem.GetComponent<EventSy
    }

    public void noinputMe(){
        MonoBehaviour[] comps = GetComponents<MonoBehaviour>();
        foreach(MonoBehaviour c in comps){
            c.enabled = false;
        }
        //EventSystem.GetComponent<EventSy
    }

}
