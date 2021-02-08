using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchController : MonoBehaviour
{

    public SpriteRenderer spriteRenderer;
    public Sprite newSprite;
    public bool isOn = false;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }


    public void ChangeSprite(Sprite paramSprite)
    {
        spriteRenderer.sprite = newSprite;
    }

}
