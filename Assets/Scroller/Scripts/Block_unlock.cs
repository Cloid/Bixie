using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_unlock : MonoBehaviour
{
    public bool isActive = false;

    private int triggerCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isActive = FindObjectOfType<TorchControllerSS>().isLit;
        print(isActive);

        gameObject.SetActive(!isActive);

        //gameObject.GetComponent<BoxCollider>().enabled = isActive;
        // print(triggerCount);
    }
}