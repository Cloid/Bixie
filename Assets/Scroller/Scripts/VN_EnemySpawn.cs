using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VN_EnemySpawn : MonoBehaviour {
//Made by Sheng Yu, adapted for VN by Cloid
	public float maxZ, minZ;
	public GameObject[] enemy;
	public int numberOfEnemies;
	public float spawnTime;

	private int currentEnemies;
	private MusicController music;
    public GameObject Tablet;
	// Use this for initialization
	void Start () {
		music = FindObjectOfType<MusicController>();
	}
	
	// Update is called once per frame
	void Update () {
		
		if(currentEnemies >= numberOfEnemies)
		{
			int enemies = FindObjectsOfType<Enemy>().Length;
			if(enemies <= 0)
			{
                Tablet.gameObject.SetActive(true);
			}
		}

	}

	void SpawnEnemy()
	{
		bool positionX = Random.Range(0, 1) == 0 ? true : false;
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
			FindObjectOfType<CameraFollow>().maxXAndY.x = transform.position.x;
			SpawnEnemy();
		}
	}
}
