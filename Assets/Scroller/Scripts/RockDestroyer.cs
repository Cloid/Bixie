using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockDestroyer : MonoBehaviour
{
    public GameObject Tablet1;
    public GameObject Tablet2;
    public GameObject Tablet3;
    public GameObject Rock1;
    public GameObject Rock2;
    public GameObject Rock3;
    public GameObject Rock4;


    // Start is called before the first frame update
    void Start()
    {
        Tablet1 = GameObject.Find("Tablet1");
        Tablet2 = GameObject.Find("Tablet2");
        Tablet3 = GameObject.Find("Tablet3");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
