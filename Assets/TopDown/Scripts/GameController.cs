using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{
    public static GameController instance;
    private static float health = 10;
    private static int maxHealth = 10;
    private static float moveSpeed = 5f;
    private static float fireRate = 0.5f;
    private static float bulletSize = 0.5f;

    // Global Variables for Game
    // isSingleplayer -- Game will be singleplayer but with 2 characters
    // isMultiplayer -- 2 people control each character

    // Global Variables for Players
    public static float  Health { get => health; set => health = value; }
    public static int MaxHealth { get => maxHealth; set => maxHealth = value;}
    public static float MoveSpeed { get => moveSpeed; set => moveSpeed = value;}
    public static float FireRate { get => fireRate; set => fireRate = value;}
    public static float BulletSize { get => bulletSize; set => bulletSize = value;}

    // Player Variables
    public GameObject player1;
    public GameObject player2;
    public static AudioSource hurtSound;

    // Game UI Variables
    public Text healthText;
    public static Text gameOverText;
    public static Button gameOverButton;

    // Camera Variables
    public Camera mCamera;
    Vector3 mCamPosition;


    // Awake is called before the game is run
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if(instance == null){
           instance = this;
        }

       // Gets button component
        gameOverButton = GetComponent<Button>();

        // Gets text component
        gameOverText = GetComponent<Text>();

        // Init mCamera
        mCamera = Camera.main;

        // Get sound component
        hurtSound = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    private void Start(){
        // Find player objects
        player1 = GameObject.Find("Player");
        player2 = GameObject.Find("Player2");

        // Camera Functionality
        mCamPosition = new Vector3 (player1.transform.position.x, player1.transform.position.y, -9);

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

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SceneManager.LoadScene("TutorialLevel");
        } else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SceneManager.LoadScene("Level2");
        } else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SceneManager.LoadScene("Level2B");
        } else if (Input.GetKeyDown(KeyCode.Alpha4)){
            SceneManager.LoadScene("Simulator");
        } else if (Input.GetKeyDown(KeyCode.Alpha5)){
            SceneManager.LoadScene("Intro");
        }

        healthText.text = "Health: " + health;

        // If R is pressed, switches players and camera is adjusted to the next player
        if (Input.GetKeyDown(KeyCode.R)){
            switchPlayer();
            if (PlayerController.isActive){
                mCamera.transform.SetParent(player1.transform);
                mCamPosition.x = player1.transform.position.x;
                mCamPosition.y = player1.transform.position.y;
                mCamera.transform.position = mCamPosition;
            } else {
                mCamera.transform.SetParent(player2.transform);
                mCamPosition.x = player2.transform.position.x;
                mCamPosition.y = player2.transform.position.y;
                mCamera.transform.position = mCamPosition;
            }
        }


    }


    // Switches players in singleplayer
    private static void switchPlayer(){
        Debug.Log("Switching players!");
        PlayerController.setActive();
        Player2Controller.setActive();
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
        Debug.Log("Player has been hurt!");
        hurtSound.Play();
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

    public static void LightLantern(){

    }

    public static void HealPlayer(float healAmount){
        health = Mathf.Min(maxHealth, health+healAmount);
    }

    // Activates game over screen
    private static void KillPlayer(){
        Debug.Log("Players have been killed!");
        gameOverText.gameObject.SetActive(true);
        gameOverButton.gameObject.SetActive(true);
        Time.timeScale = 0;
        gameOverButton.onClick.AddListener(StartOver);
    }

    // Resets the scene
    private static void StartOver(){
        Debug.Log("Time to start over!");
        Scene scene = SceneManager.GetActiveScene();
        Time.timeScale = 1;
        Reset();
        SceneManager.LoadScene(scene.name);
    }


}
