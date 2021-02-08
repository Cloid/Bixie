using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SimManager : MonoBehaviour
{
    public GameObject speedText;
    public GameObject p2speedText;

    public GameObject rateText;
    public GameObject bulletSizeText;

    //public GameObject player1;
    //private GameController gameController;

    void Start() {
        //gameController = FindObjectOfType<GameController>();
    }
    // Update is called once per frame
    void Update()
    {
        speedText.GetComponent<Text>().text = GameController.MoveSpeed.ToString();
        p2speedText.GetComponent<Text>().text = GameController.MoveSpeed.ToString();
        rateText.GetComponent<Text>().text = GameController.FireRate.ToString();  
        bulletSizeText.GetComponent<Text>().text = GameController.BulletSize.ToString();
    }


}
