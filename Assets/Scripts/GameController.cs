using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{
    public static GameController instance;
    private static float health = 6;
    private static int maxHealth = 6;
    private static float moveSpeed = 5f;
    private static float fireRate = 0.5f;
    private static float bulletSize = 0.5f;

    // Global Variables for Players
    public static float  Health { get => health; set => health = value; }
    public static int MaxHealth { get => maxHealth; set => maxHealth = value;}
    public static float MoveSpeed { get => moveSpeed; set => moveSpeed = value;}
    public static float FireRate { get => fireRate; set => fireRate = value;}
    public static float BulletSize { get => bulletSize; set => bulletSize = value;}

    // Game UI 
    public Text healthText;
    public static Text gameOverText;
    public static Button gameOverButton;

    // Awake is called before the game is run
    private void Awake()
    {
       if(instance == null){
           instance = this;
       }

       // Gets button component
        gameOverButton = GetComponent<Button>();

        // Gets text component
        gameOverText = GetComponent<Text>();
    }

    // Start is called before the first frame update
    private void Start(){
        // Game Over Button Functionality
        // Finds object, changes text to start over, then deactivates button until player is killed
        gameOverButton = GameObject.Find("RestartButton").GetComponent<Button>();
        gameOverButton.GetComponentInChildren<Text>().text = "Start Over";
        gameOverButton.gameObject.SetActive(false);

        // Game Over Text Functionality
        gameOverText = GameObject.Find("GameOverText").GetComponent<Text>();
        gameOverText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update(){
        healthText.text = "Health: " + health;
    }

    // If the player starts over, trigger this method to reset all of the player variables
    private static void Reset()
    {
        Debug.Log("Has been reset!");
        health = 6;
        maxHealth = 6;
        moveSpeed = 5f;
        fireRate = 0.5f;
        bulletSize = 0.5f;
}

    public static void DamagePlayer(int damage){
        health -= damage;
        
        if(Health <= 0){
            KillPlayer();
        }
    }

    public static void MoveSpeedChange(float speed){
        moveSpeed += speed;
    }

    public static void FireRateChange(float rate){
        fireRate -= rate;
    }

    public static void BulletSizeChange(float size){
        bulletSize += size;
    }


    public static void HealPlayer(float healAmount){
        health = Mathf.Min(maxHealth, health+healAmount);
    }

    private static void KillPlayer(){
        Debug.Log("Players have been killed!");
        gameOverText.gameObject.SetActive(true);
        gameOverButton.gameObject.SetActive(true);
        Time.timeScale = 0;
        gameOverButton.onClick.AddListener(StartOver);
    }

    private static void StartOver(){
        Debug.Log("Time to start over!");
        Scene scene = SceneManager.GetActiveScene();
        Time.timeScale = 1;
        Reset();
        SceneManager.LoadScene(scene.name);
    }

}
