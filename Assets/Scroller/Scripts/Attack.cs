using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Attack : MonoBehaviour {

	public float damage;
	public string attackState;
	public float attackDirection;
	public PhotonView photonView;

	// Use this for initialization

	private void Start() {
		photonView = GetComponent<PhotonView>();
	}
	
	private void OnTriggerEnter(Collider other)
	{
		if(photonView.IsMine){
		Enemy enemy = other.GetComponent<Enemy>();
		Player player = other.GetComponent<Player>();
		Player2 player2 = other.GetComponent<Player2>();
		CageController cage = other.GetComponent<CageController>();
		RockSmashController rock = other.GetComponent<RockSmashController>();
		// Projectile playerProjectile = other.GetComponent<Projectile>();
		if(enemy != null)
		{
			enemy.photonView.RPC("TookDamage",RpcTarget.AllBuffered,damage,attackState,"",attackDirection);
		}

		if (player != null)
		{
			player.photonView.RPC("TookDamage", RpcTarget.AllBuffered,damage);
		}

		if (player2 != null)
		{
			player2.photonView.RPC("TookDamage", RpcTarget.AllBuffered,damage);
		}

		if(cage != null){
			cage.TookDamage(damage);
		}

		if(rock != null){
			Debug.Log("ROCK HIT");
			rock.photonView.RPC("DestroyRock",RpcTarget.All);
		}
		}

	}
}
