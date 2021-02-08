using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingShit : MonoBehaviour
{
    public bool enemyTrigger = false;
    public GameObject Tablet;
   
    // Update is called once per frame
    void Update()
    {
        if(enemyTrigger == true){
            Debug.Log("Enemy");
            Tablet.SetActive(false);
        }
        
        if(enemyTrigger == false){
            Debug.Log("Player");
            Tablet.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other){
        Debug.Log("Calling: " + other);
        if(other.gameObject.CompareTag("Player")){
            enemyTrigger = false;
        }

        if(other.gameObject.CompareTag("Player2")){
            enemyTrigger = false;
        }

        if(other.gameObject.CompareTag("Enemy")){
            enemyTrigger = true;
        }
    }

}
