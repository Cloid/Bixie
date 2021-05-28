using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus; 

public class VNTrigger : MonoBehaviour
{
    public Flowchart VNController;
    public string Block;
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player" || other.tag == "Player2"){
            VNController.ExecuteBlock(Block);
            gameObject.SetActive(false);
        }
    }
}
