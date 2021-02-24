using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchControllerSS : MonoBehaviour
{
    // Attributes
    public GameObject torch;

    public GameObject bamboo;
    SpriteRenderer sprite;
    public Sprite darkSprite;
    public Sprite litSprite;
    public bool isLit;
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
        print("Lantern lit!");
        sprite.sprite = litSprite;
        isLit = true;
        Destroy(bamboo);
    }

    public void darkLantern()
    {
        print("Lantern dark!");
        sprite.sprite = darkSprite;
        isLit = false;
    }
}
