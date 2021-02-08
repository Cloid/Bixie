using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NButton : MonoBehaviour
{
    // Start is called before the first frame update
    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Worked");
            SceneManager.LoadScene("Level2");

        }
    }
}
