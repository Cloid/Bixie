using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class TorchControllerSS : MonoBehaviour
{
    // Attributes
    public GameObject torch;

    public GameObject bamboo;
    SpriteRenderer sprite;
    public Sprite darkSprite;
    public Sprite litSprite;
    public bool isLit;
    public GameObject EnemySpawn;

    public Flowchart Test;
    // Start is called before the first frame update
    void Start()
    {
        // Initialization
        sprite = torch.GetComponent<SpriteRenderer>();
        isLit = false;
    }

    // Update is called once per frame
    void Update()
    {
        // print("actually lit: " + isLit);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player2")){
            lightLantern();
        }
    }

    // Lights lantern
    public void lightLantern()
    {
        if (isLit == false) {
            print("Lantern lit!");
            Test.StopAllBlocks();
            Test.ExecuteBlock("blah");
            sprite.sprite = litSprite;
            isLit = true;

            // Play FMOD lit lantern sound
            FMODUnity.RuntimeManager.PlayOneShot("event:/Sounds/Lantern_Ignite", GetComponent<Transform>().position);

            Destroy(bamboo);
            if(EnemySpawn != null){
                Destroy(EnemySpawn);
            }
        }
    }

    public void darkLantern()
    {
        print("Lantern dark!");
        sprite.sprite = darkSprite;
        isLit = false;
    }
}
