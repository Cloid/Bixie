using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

	public int damage;
	public string attackState;
	public float attackDirection;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		

		
	}

	private void OnTriggerEnter(Collider other)
	{
		Enemy enemy = other.GetComponent<Enemy>();
		Player player = other.GetComponent<Player>();
		Player2 player2 = other.GetComponent<Player2>();
		// Projectile playerProjectile = other.GetComponent<Projectile>();
		if(enemy != null)
		{
			enemy.TookDamage(damage, attackState, attackDirection);
		}

		if (player != null)
		{
			player.TookDamage(damage);
		}

		if (player2 != null)
		{
			player2.TookDamage(damage);
		}
	}
}
