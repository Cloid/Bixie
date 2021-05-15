using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetCameraScript : MonoBehaviour {

	public void Activate()
	{
		
		GetComponent<Animator>().SetTrigger("Go");

		// Go Frog Sound
		FMODUnity.RuntimeManager.PlayOneShot("event:/Sounds/Go", Vector3.right * 200);
	}

	void ResetCamera()
	{
		FindObjectOfType<CameraFollow>().maxXAndY.x = 270;
	}

}
