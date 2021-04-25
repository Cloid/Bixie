using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TabletController : MonoBehaviour
{
    Animator Anim;
    //public UIWindow WindowHeader;
    public string DialogueText;
    public DialogueWindow Dialogue;
    // Start is called before the first frame update
    void Start()
    {
        //Anim = GetComponent<Animator>();
        //Debug.Log(DialogueText);
        //Dialogue.Show(DialogueText);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player"){
            //Anim.SetBool("Open", true);
            Dialogue.Show(DialogueText);
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.tag == "Player"){
        //Anim.SetBool("Open",false);
        Dialogue.Close();
        }    
    }

}
