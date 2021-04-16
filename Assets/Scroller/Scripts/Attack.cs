﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Attack : MonoBehaviour {

	public int damage;
	public string attackState;
	public float attackDirection;

	// Use this for initialization

	private void OnTriggerEnter(Collider other)
	{
		Enemy enemy = other.GetComponent<Enemy>();
		Player player = other.GetComponent<Player>();
		Player2 player2 = other.GetComponent<Player2>();
		CageController cage = other.GetComponent<CageController>();
		// Projectile playerProjectile = other.GetComponent<Projectile>();
		if(enemy != null)
		{
			//enemy.TookDamage(damage, attackState, attackDirection);
			enemy.photonView.RPC("TookDamage",RpcTarget.AllBuffered,damage,attackState,attackDirection);
		}

		if (player != null)
		{
			//player.TookDamage(damage);
			player.photonView.RPC("TookDamage", RpcTarget.AllBuffered,damage);
		}

		if (player2 != null)
		{
			//player2.TookDamage(damage);
			player2.photonView.RPC("TookDamage", RpcTarget.AllBuffered,damage);
		}

		if(cage != null){
			cage.TookDamage(damage);
		}

	}
}
