using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstWall : MonoBehaviour
{
    public GameObject Wall;
    public GameObject Enemy;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if(!Enemy.activeSelf){
            Destroy(Wall);
        }
    }
}
