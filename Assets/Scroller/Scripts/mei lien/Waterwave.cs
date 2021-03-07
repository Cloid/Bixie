using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waterwave : MonoBehaviour
{

	public int direction = 1;
	public int speed = 6;

	private Rigidbody rb;

	// Use this for initialization
	void Start()
	{

		rb = GetComponent<Rigidbody>();
		StartCoroutine(MoveWaterwave());
	}

	// Update is called once per frame
	void FixedUpdate()
	{

		rb.velocity = new Vector3(speed * direction, 0, 2 * direction);

	}

	IEnumerator MoveWaterwave()
	{
		yield return new WaitForSeconds(2f);
		direction *= -1;
	}
}
