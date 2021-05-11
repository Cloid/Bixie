using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LitBar : MonoBehaviour
{
    public int max;
    public int min;
    public Image mask;
    public int ritualTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetCurrentFill();
    }

    public void GetCurrentFill()
    {
        // float fillAmount = (floatcurre)
        mask.fillAmount -= 1.0f / ritualTime * Time.deltaTime;
    }
}
