using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	/* Future goals
	 * Create a directory of game objects (Torch, Doors, etc.)
	 * Both player scripts should access these game objects
	*/
	// Attributes
	public int lives;
	public int characterIndex;
	
	// GameObjects
	//private Player player;
	//private Player2 player2;
	//public GameObject torch;
	private static GameManager gameManager;

	// Use this for initialization
	void Awake () {
		
		if(gameManager == null)
		{
			gameManager = this;
		}
		else if(gameManager != this)
		{
			Destroy(gameObject);
		}

		DontDestroyOnLoad(gameObject);
	}

	// Starts before first frame
    void Start()
    {
	}

    // Update is called once per frame
    void Update () {
		
	}
}
