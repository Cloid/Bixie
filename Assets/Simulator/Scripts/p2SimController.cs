using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class p2SimController : MonoBehaviour {

    // Array of waypoints to walk from one to the next one
    [SerializeField]
    private Transform[] waypoints;

    // Walk speed that can be set in Inspector
    [SerializeField]
    private float moveSpeed = 2f;

    // Index of current waypoint from which Enemy walks
    // to the next one
    private int waypointIndex = 0;

    // If player has no inputted control
    private bool playerControl = false;

    // Waypoint has been reached
    private bool finishedWaypoint = false;

    // Used for player input
    private Rigidbody2D rigidbody;


	// Use this for initialization
	private void Start () {

        // Set position of Enemy as position of the first waypoint
        transform.position = waypoints[waypointIndex].transform.position;
        rigidbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	private void Update () {
        //Grabbing input axis for player control
        float horizontal = Input.GetAxis("p2Horizontal");
        float vertical = Input.GetAxis("p2Vertical");
        rigidbody.velocity = new Vector3(horizontal * 3, vertical * 3, 0);

        if(horizontal != 0 || vertical != 0){
            playerControl = true;
        }


        if(playerControl == false && finishedWaypoint == false){
        // Move Enemy
            Move();
        } else if (finishedWaypoint == true){
            MoveBack();
        }

	}

    // Method that actually make Enemy walk
    private void Move()
    {
        // If Enemy didn't reach last waypoint it can move
        // If enemy reached last waypoint then it stops
        if (waypointIndex <= waypoints.Length - 1)
        {

            // Move Enemy from current waypoint to the next one
            // using MoveTowards method
            transform.position = Vector2.MoveTowards(transform.position,
               waypoints[waypointIndex].transform.position,
               moveSpeed * Time.deltaTime);

            // If Enemy reaches position of waypoint he walked towards
            // then waypointIndex is increased by 1
            // and Enemy starts to walk to the next waypoint
            if (transform.position == waypoints[waypointIndex].transform.position)
            {
                waypointIndex += 1;
            }
            
            if(waypointIndex >= waypoints.Length){
                finishedWaypoint = true;
                waypointIndex -=1;
            }

        }
    }

    private void MoveBack()
    {
            // Move Enemy from current waypoint to the next one
            // using MoveTowards method
            transform.position = Vector2.MoveTowards(transform.position,
               waypoints[waypointIndex].transform.position,
               moveSpeed * Time.deltaTime);

            // If Enemy reaches position of waypoint he walked towards
            // then waypointIndex is increased by 1
            // and Enemy starts to walk to the next waypoint
            if (transform.position == waypoints[waypointIndex].transform.position)
            {
                waypointIndex -= 1;
            }

            if(waypointIndex == -1){
                finishedWaypoint = false;
                waypointIndex += 1;
            }
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Door"){
            SceneManager.LoadScene("MidScene");
        }
    }

}
