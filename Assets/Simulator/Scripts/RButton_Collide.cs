using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RButton_Collide : MonoBehaviour
{
    public int train = 0;

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Training");
            train++; 
            if (train >= 3)
            {
                GameController.MoveSpeedChange(1);
                train = 0;
            }

        }
    }
    }
