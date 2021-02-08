using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2Wander : MonoBehaviour
{
    Vector3 pointA = new Vector3(-2.9230001f, -2.18600011f, -0.356552511f);

    Vector3 pointB = new Vector3(1.06f, -2.18600011f, -0.356552511f);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(pointA, pointB, Mathf.PingPong(Time.time, 1));

    }
}
