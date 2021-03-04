using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Control : MonoBehaviour
{
    public GameObject ml_sel;
    public GameObject ml_unsel;
    public GameObject q_sel;
    public GameObject q_unsel;
    public GameObject blur;
    private bool ml_on = false;
    private bool q_on = false;

    public bool p1_selected = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void moveMe(Vector2 vector)
    {
        Debug.Log(vector);
        if (vector.x == -1 && (!ml_on))
        {
            blur.SetActive(true);
            q_sel.SetActive(false);
            //ml_unsel.SetActive(false);
            ml_sel.SetActive(true);
            ml_on = true;
            q_on = false;
        }
        else if (vector.x == 1 && (!q_on))
        {
            blur.SetActive(true);
            ml_sel.SetActive(false);
            //q_unsel.SetActive(false);
            q_sel.SetActive(true);
            ml_on = false;
            q_on = true;
        }
    }

    public void charSelect(){
        if(ml_sel.activeSelf == true){

        } else {
            
        }
    }
}
