using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float lifeTime;
    // Start is called before the first frame update
    void Start()
    {
        // Coroutine are liked timed functions
        StartCoroutine(DeathDelay());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DeathDelay(){
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

}
