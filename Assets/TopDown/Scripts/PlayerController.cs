﻿using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // Declare whether Player1 is being controlled - currently inactive for multiplayer
    public static bool isActive;

    // Declare a global variable for speed, can be edited within Unity and accessed everywhere
    public float speed;

    // Deckare a rigidbody for reference later on
    new Rigidbody2D rigidbody;
    public Text collectedText;
    public static int collectedAmount = 0;

    // Player attributes
    public GameObject bulletPrefab;
    public float bulletSpeed;
    private float lastFire;
    public float fireDelay;
    public float specialDelay;
    public AudioSource fireballSound;

    // Start is called before the first frame update
    void Start()
    {
        isActive = true;
        // Connect rigidbody to whatever component it's connected to
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Shooting mechanic
        fireDelay = GameController.FireRate;
        float shootHor = Input.GetAxis("ShootHorizontal");
        float shootVert = Input.GetAxis("ShootVertical");

        if ((shootHor != 0 || shootVert != 0) && Time.time > lastFire + fireDelay)
        {
            fireballSound.Play();
            Shoot(shootHor, shootVert);
            lastFire = Time.time;
        }
        collectedText.text = "Items Collected: " + collectedAmount;

        // Special ability mechanic
        if (Input.GetKeyDown(KeyCode.Q) && Time.time > lastFire + specialDelay)
        {
            Special();
            lastFire = Time.time;
        }

        // Determine movement
        speed = GameController.MoveSpeed;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        rigidbody.velocity = new Vector3(horizontal * speed, vertical * speed, 0);
    }

    // Shooting function 
    void Shoot(float x, float y)
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
        bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(
            // Temp operator to check for conditional, here checking if x < 0 and if it is, floor the x value. If not, get the cieling of the x value.
            (x < 0) ? Mathf.Floor(x) * bulletSpeed : Mathf.Ceil(x) * bulletSpeed,
            (y < 0) ? Mathf.Floor(y) * bulletSpeed : Mathf.Ceil(y) * bulletSpeed,
            0
        );
    }

    // Special ability function 
    void Special()
    {
        GameController.HealPlayer(2);
    }

    // Set active function - currently inactive
    public static void setActive()
    {
        if (isActive){
            isActive = false;
        } else {
            isActive = true;
        }
    }
}
