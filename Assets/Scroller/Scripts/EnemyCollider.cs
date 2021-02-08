using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollider : MonoBehaviour
{
    public GameObject Tablet;
    public GameObject EnemySpawn;
    public bool enemyCollide;
    // Start is called before the first frame update
    void Start() {
        //Tablet1 = GameObject.Find("Tablet1");
        //Tablet2 = GameObject.Find("Tablet2");    
        //Tablet3 = GameObject.Find("Tablet3");     
    }

    private void Update() {
        if(enemyCollide == false){
            Tablet.SetActive(true);
        }

        if(enemyCollide == true){
            Tablet.SetActive(false);
        }
    }
    void OnTriggerStay(Collider other){

        //enemyCollide = true;

        if(other.tag == "Player" || other.tag == "Player2"){
            Debug.Log("Calling");
            if(!EnemySpawn.activeSelf){
                Debug.Log("Fucking Work");
                enemyCollide = false;
            }

        }

        if(EnemySpawn.activeSelf){
            enemyCollide = true;
        }        

    }

}
