using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resume : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResumeScene(){
        Time.timeScale = 1;
        Debug.Log("Called");
        //Destroy(gameObject);
        //gameObject.SetActive(false);
    }
}
