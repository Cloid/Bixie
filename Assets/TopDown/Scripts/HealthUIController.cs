using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUIController : MonoBehaviour
{
    public GameObject heartContainer;
    private float fillValue;
    private float oldValue;
    //public float EaseTime = 10F;

    // Update is called once per frame
    void Update()
    {
        fillValue = (float)GameController.Health;
        fillValue = fillValue / GameController.MaxHealth;
        print(fillValue);
        oldValue = heartContainer.GetComponent<Image>().fillAmount;
        heartContainer.GetComponent<Image>().fillAmount = Mathf.Lerp(oldValue,fillValue, Time.deltaTime);
        // print(heartContainer.GetComponent<Image>().fillAmount);
        
    }
}
