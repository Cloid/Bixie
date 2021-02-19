using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class TabletTrigger : MonoBehaviour
{
    public Flowchart Tablet;

    public void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player2"){
            Tablet.ExecuteBlock("TabletCall");
        }
    }
    
}
