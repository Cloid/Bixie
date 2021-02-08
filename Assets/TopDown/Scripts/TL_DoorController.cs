using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TL_DoorController : MonoBehaviour
{
    public GameObject Door_1;
    public GameObject Door_2;
    public GameObject Door_3;

    public GameObject Torch_1;
    public GameObject Torch_2;
    public GameObject Torch_3;


    // Update is called once per frame
    void Update()
    {

        if (Torch_1.gameObject.GetComponent<TorchController>().isOn)
        {
            if (Door_1)
            {
                Destroy(Door_1);
            }
        }

        if (Torch_2.gameObject.GetComponent<TorchController>().isOn)
        {
            if (Door_2)
            {
                Destroy(Door_2);
            }
            
        }

        if (Torch_3.gameObject.GetComponent<TorchController>().isOn)
        {
            if (Door_3)
            {
                Destroy(Door_3);
            }
        }
    }
}
