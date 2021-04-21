// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class CameraFollow : MonoBehaviour {

// 	public float xMargin = 1f; // Distance in the x axis the player can move before the camera follows.
// 	// public float yMargin = 1f; // Distance in the y axis the player can move before the camera follows.
// 	public float xSmooth = 8f; // How smoothly the camera catches up with it's target movement in the x axis.
// 	public float ySmooth = 8f; // How smoothly the camera catches up with it's target movement in the y axis.
// 	public Vector2 maxXAndY; // The maximum x and y coordinates the camera can have.
// 	public Vector2 minXAndY; // The minimum x and y coordinates the camera can have.

// 	private Transform player1; // Reference to the player's transform.


// 	private void Awake()
// 	{
// 		// Setting up the reference.
// 		player1 = GameObject.FindGameObjectWithTag("Player").transform;
// 	}


// 	private bool CheckXMarginRight()
// 	{
// 		// Returns true if the distance between the camera and the player in the x axis is greater than the x margin.
// 		return (transform.position.x - player1.position.x) < xMargin;
// 	}

// 	private bool CheckXMarginLeft()
// 	{
// 		// Returns true if the distance between the camera and the player in the x axis is greater than the x margin.
// 		return (transform.position.x - player1.position.x) > xMargin;
// 	}


// 	// private bool CheckYMarginDown()
// 	// {
// 	// 	// Returns true if the distance between the camera and the player in the y axis is greater than the y margin.
// 	// 	return Mathf.Abs(transform.position.z - player1.position.z) < yMargin;
// 	// }

// 	// private bool CheckYMarginUp()
// 	// {
// 	// 	// Returns true if the distance between the camera and the player in the y axis is greater than the y margin.
// 	// 	return Mathf.Abs(transform.position.z - player1.position.z) > 6;
// 	// }


// 	private void Update()
// 	{
// 		TrackPlayer();
// 	}


// 	private void TrackPlayer()
// 	{
// 		// By default the target x and y coordinates of the camera are it's current x and y coordinates.
// 		float targetX = transform.position.x;
// 		float targetY = transform.position.y;

		

// 		// print(targetX);
// 		// print(targetY);
// 		// print("up: "+CheckYMarginUp());
// 		// print("down: "+CheckYMarginDown());
// 		print(transform.position.x);
// 		print(player1.position.x);
// 		// If the player has moved beyond the x margin...
// 		if (CheckXMarginRight())
// 		{
// 			// ... the target x coordinate should be a Lerp between the camera's current x position and the player's current x position.
// 			targetX = Mathf.Lerp(transform.position.x, player1.position.x, xSmooth * Time.deltaTime);
// 		}

// 		if (CheckXMarginLeft())
// 		{
// 			// ... the target x coordinate should be a Lerp between the camera's current x position and the player's current x position.
// 			targetX = Mathf.Lerp(transform.position.x, player1.position.x, -xSmooth * Time.deltaTime);
// 		}

// 		// If the player has moved beyond the y margin...
// 		// if (CheckYMarginDown())
// 		// {
// 		// 	// ... the target y coordinate should be a Lerp between the camera's current y position and the player's current y position.
// 		// 	targetY = Mathf.Lerp(transform.position.y, player1.position.z, ySmooth * Time.deltaTime);
// 		// }

// 		// if (CheckYMarginUp())
// 		// {
// 		// 	// ... the target y coordinate should be a Lerp between the camera's current y position and the player's current y position.
// 		// 	targetY = Mathf.Lerp(player1.position.z, transform.position.y, ySmooth * Time.deltaTime);
// 		// }

// 		// The target x and y coordinates should not be larger than the maximum or smaller than the minimum.
// 		targetX = Mathf.Clamp(targetX, minXAndY.x, maxXAndY.x);
// 		// targetY = Mathf.Clamp(targetY, minXAndY.y, maxXAndY.y);

// 		// Set the camera's position to the target position with the same z component.
// 		transform.position = new Vector3(targetX, transform.position.y, transform.position.z);
// 	}
// }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public float xMargin = 1f; // Distance in the x axis the player can move before the camera follows.
	// public float yMargin = 1f; // Distance in the y axis the player can move before the camera follows.
	public float xSmooth = 8f; // How smoothly the camera catches up with it's target movement in the x axis.
	public float ySmooth = 8f; // How smoothly the camera catches up with it's target movement in the y axis.

    public float posY;
    public float smoothTime = 0.3F;
	public Vector2 maxXAndY; // The maximum x and y coordinates the camera can have.
	public Vector2 minXAndY; // The minimum x and y coordinates the camera can have.

	public float alwaysY = 11.3f;

	private bool zoomActive = false;
	private Camera Cam;
	private Transform player1; // Reference to the player1's transform.
	private Transform player2; // Reference to the player2's transform.
    private Vector3 velocity = Vector3.zero;

	private void Awake()
	{
		// Setting up the reference.
		Cam = Camera.main;
	}

	private bool CheckXMarginRight()
	{
		// Returns true if the distance between the camera and the player in the x axis is greater than the x margin.
		return (transform.position.x - player1.position.x) < xMargin;
	}

	private bool CheckXMarginLeft()
	{
		// Returns true if the distance between the camera and the player in the x axis is greater than the x margin.
		return (transform.position.x - player1.position.x) > xMargin;
	}


	// private bool CheckYMarginDown()
	// {
	// 	// Returns true if the distance between the camera and the player in the y axis is greater than the y margin.
	// 	return Mathf.Abs(transform.position.z - player1.position.z) < yMargin;
	// }

	// private bool CheckYMarginUp()
	// {
	// 	// Returns true if the distance between the camera and the player in the y axis is greater than the y margin.
	// 	return Mathf.Abs(transform.position.z - player1.position.z) > 6;
	// }


	private void Update()
	{
		if (player1 == null) player1 = GameObject.FindGameObjectWithTag("Player").transform;
			
		if (player2 == null) player2 = GameObject.FindGameObjectWithTag("Player2").transform;
	
		if(player1 != null && player2 != null){
			TrackPlayer();
		}
	}

	private void ResetY(){
		gameObject.transform.position = new Vector3(transform.position.x, alwaysY, transform.position.z);
	}
	private void TrackPlayer()
	{
		// By default the target x and y coordinates of the camera are it's current x and y coordinates.
		float targetX = transform.position.x;
		float targetY = transform.position.y;
		
		// Vector3 targetPostition = player1.TransformPoint(new Vector3(0, posY, -10));
		Vector3 middle = (player1.position + player2.position) * 0.5f;
        Vector3 desiredPosition = Vector3.SmoothDamp(transform.position, middle, ref velocity, smoothTime);


		// print(targetX);
		// print(targetY);
		// print("up: "+CheckYMarginUp());
		// print("down: "+CheckYMarginDown());
		
		// The target x and y coordinates should not be larger than the maximum or smaller than the minimum.
		targetX = Mathf.Clamp(desiredPosition.x, minXAndY.x, maxXAndY.x);
		// targetY = Mathf.Clamp(targetY, minXAndY.y, maxXAndY.y);

		// Set the camera's position to the target position with the same z component.
		transform.position = new Vector3(targetX, transform.position.y, transform.position.z);
	}

	public void LateUpdate() {
		if(zoomActive){
			
		}else{

		}
	}
}
