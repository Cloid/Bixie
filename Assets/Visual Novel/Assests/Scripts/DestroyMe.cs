using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
public class DestroyMe : MonoBehaviour
{
    public Flowchart Resume;
    public AudioOptions incNum;
    public PauseInput IntroScene;
    // Start is called before the first frame update
    void Start()
    {
        Resume = GameObject.Find("PauseFlowchart").GetComponent<Flowchart>();
        incNum = GameObject.Find("AudioController").GetComponent<AudioOptions>();
        IntroScene = GameObject.Find("PauseFlowchart").GetComponent<PauseInput>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Resume == null){
            Resume = GameObject.Find("PauseFlowchart").GetComponent<Flowchart>();
        }

        if(incNum == null){
            incNum = GameObject.Find("AudioController").GetComponent<AudioOptions>();
        }

        if(IntroScene == null){
            IntroScene = GameObject.Find("PauseFlowchart").GetComponent<PauseInput>();
        }
        
    }

    public void DestroyScene(){

        incNum.incNum();
        if(incNum.sceneNum == 0){
            Destroy(gameObject);
            IntroScene.IntroScene();
            IntroScene.ResetBool();
        } else if(incNum.sceneNum == 2){
            Destroy(gameObject);
            Resume.ExecuteBlock("Pause");
            IntroScene.ResetBool();
        } else {
            Destroy(gameObject);
            Resume.ExecuteBlock("Pause_nonVN");
            IntroScene.ResetBool();
        }
        
    }

}
