using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {

	public float maxZ, minZ;
	public GameObject[] enemy;
	public int numberOfEnemies;
	public float spawnTime;

	private int currentEnemies;
	private MusicController music;
	// Use this for initialization
	void Start () {
		music = FindObjectOfType<MusicController>();
	}
	
	// Update is called once per frame
	void Update () {
        //print("currentEnemies: " + currentEnemies);
        //print("numberOfEnemies: " + numberOfEnemies);
        if (currentEnemies >= numberOfEnemies)
		{
			int enemies = FindObjectsOfType<Enemy>().Length;
			if (enemies <= 0)
			{
				FindObjectOfType<ResetCameraScript>().Activate();
				gameObject.SetActive(false);
			}
		}

	}

	void SpawnEnemy()
	{
		bool positionX = Random.Range(0, 2) == 0 ? true : false;
		Vector3 spawnPosition;
		spawnPosition.z = Random.Range(minZ, maxZ);
		if (positionX)
		{
			spawnPosition = new Vector3(transform.position.x + 10, 0, spawnPosition.z);
		}
		else
		{
			spawnPosition = new Vector3(transform.position.x - 10, 0, spawnPosition.z);
		}
		// randomly gen enemy types
		Instantiate(enemy[Random.Range(0, enemy.Length)], spawnPosition, Quaternion.identity);
		currentEnemies++;
		if(currentEnemies < numberOfEnemies)
		{
			Invoke("SpawnEnemy", spawnTime);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			music.PlaySong(music.fightSong);
			GetComponent<BoxCollider>().enabled = false;
            // CAM: commented this out: temp solution for not locking the camera when enemies spawn
            //FindObjectOfType<CameraFollow>().maxXAndY.x = transform.position.x;
            SpawnEnemy();
		}
	}
}
