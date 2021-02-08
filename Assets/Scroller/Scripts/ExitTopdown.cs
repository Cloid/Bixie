using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ExitTopdown : MonoBehaviour
{
    
    public bool isActive = false;

    private int triggerCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        // gameObject.SetActive(isActive);
    }

    // Update is called once per frame
    void Update()
    {
        isActive = FindObjectOfType<TorchControllerSS>().isLit;
        // print(isActive);
        gameObject.GetComponent<BoxCollider>().enabled = isActive;
        // print(triggerCount);
    }

     // Use this for initialization
    private void OnTriggerEnter(Collider other)
    {
        // print("in");
        if ((other.gameObject.tag == "Player") )
        {
            triggerCount++;
            
        }

        if((other.CompareTag("Player2")))
        {
            triggerCount++;
        }

        if(triggerCount == 2)
        {
            Exit();
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if ((other.gameObject.tag == "Player"))
        {
            triggerCount--;
            
        }

        if((other.CompareTag("Player2")))
        {
            triggerCount--;
        }
    }

    private void Exit(){
        // Scene sceneToLoad = SceneManager.GetSceneByName("TutorialLevel");
        // print(sceneToLoad.name);
        // SceneManager.LoadScene(sceneToLoad.name, LoadSceneMode.Additive);
        
        StartCoroutine(FindObjectOfType<LevelLoad>().LoadLevel(4));
        // SceneManager.MoveGameObjectToScene(other.gameObject, sceneToLoad);
    }

    
}
