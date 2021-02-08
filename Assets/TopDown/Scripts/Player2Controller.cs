using UnityEngine;

public class Player2Controller : MonoBehaviour
{
    // Declare whether Player2 is being controlled - currently inactive for multiplayer
    public static bool isActive;

    // Player attributes
    public float speed2;
    private Vector3 playerDirection;
    public float attackRange;
    public Camera mCamera;
    public AudioSource meleeSound;
    private float lastTime;
    public float cooldown;

    // Declare enemy objects for melee/special attacks
    GameObject[] enemies;
    GameObject closestEnemy;
    public GameObject slashCone;

    // Declare a rigidbody for reference later on
    Rigidbody2D rigidbody2;
    private Vector3 mousePosition;

    // Start is called before the first frame update
    void Start()
    {
        // Sets mCamera
        mCamera = Camera.main;

        // Sets variables for Player2
        isActive = false;
        rigidbody2 = GetComponent<Rigidbody2D>();

        // Defines enemies for attacks
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        // Defines movement
        speed2 = GameController.MoveSpeed;

        // Movement based on mouse position
        // Code source: https://youtu.be/EiWJY9AlPkY
        mousePosition = mCamera.ScreenToWorldPoint(Input.mousePosition);
        playerDirection = (mousePosition - transform.position).normalized;
        rigidbody2.velocity = new Vector2(playerDirection.x * (speed2), playerDirection.y * (speed2));

        // Defines basic attack
        if (Input.GetMouseButtonDown(0)){
            Debug.Log("Begin melee!");
            meleeSound.Play();
            if (getClosestEnemy(enemies) <= attackRange){
                Debug.Log("Attack successful!");
                Basic(closestEnemy);
            } else {
                Debug.Log("Attack failed!");
            }
        }

        // Defines special attack
        if (Input.GetMouseButtonDown(1) && Time.time > lastTime + cooldown){
            Debug.Log("Begin special!");
            Special();
            lastTime = Time.time;
        }
    }

    // Helper function that determines closest enemy
    private float getClosestEnemy(GameObject[] enemies)
    {
        float closest = Vector3.Distance(transform.position, enemies[0].transform.position);
        closestEnemy = enemies[0];
        foreach(GameObject currEnemy in enemies){
            float currDist = Vector3.Distance(transform.position, currEnemy.transform.position);
            if(currDist < closest){
                closest = currDist;
                closestEnemy = currEnemy;
            }
        }

        return closest;
    }

    // Allows player to do a basic attack
    void Basic(GameObject enemy)
    {
        if (enemies != null){
            enemy.SetActive(false);
        }
    }
    
    // Allows player to do a special attack
    void Special()
    {
        // Instantiates the slash cone object
        Debug.Log("Creating slash cone!");
        Vector3 spawnPosition;
        Quaternion currRotation = transform.rotation;
        float currAngle = Vector3.Angle(transform.position, mousePosition);
        //currAngle = currAngle * Mathf.Rad2Deg;
        currRotation = currRotation * Quaternion.Euler(0, 0, currAngle - 30);
        if (Input.mousePosition.y - transform.position.y < 150){
            spawnPosition = new Vector3(transform.position.x, transform.position.y - 1, 1);
        } else {
            spawnPosition = new Vector3(transform.position.x, transform.position.y + 1, 1);
            currRotation = currRotation * Quaternion.Euler(0, 0, -180);
        }
        GameObject cone = Instantiate(slashCone, spawnPosition, currRotation) as GameObject;
        Destroy(cone, 1);
    }

    // Set active function - currently inactive
   public static void setActive()
    {
        if (isActive){
            isActive = false;
        }
        else{
            isActive = true;
        }
    }
}
