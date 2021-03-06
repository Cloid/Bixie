﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBlobShadow : MonoBehaviour
{

	public Transform FollowBone;
	public float BlobShadowSize = 1;
	public float DistanceScale = 2f; //the size multiplier of the blobshadow relative to the distance from the floor
	public float Yoffset = 0; //the offset of the blobshadow
	public LayerMask GroundLayerMask;
	public bool followTerrainRotation = true;
	private float rayDist = 10f; //raycast distance

	void Update()
	{
		if (FollowBone != null)
		{

			//raycast down
			RaycastHit hit;
			if (Physics.Raycast(FollowBone.transform.position, Vector3.down * rayDist, out hit, rayDist, GroundLayerMask))
			{

				//show blobshadow if we've hit something
				GetComponent<MeshRenderer>().enabled = true;

				//set position
				setPosition(hit);

				//set scale
				setScale(FollowBone.transform.position.y - hit.point.y);

				//set blobshadow rotation to hit normal
				if (followTerrainRotation)
				{
					setRotation(hit.normal);
				}

			}
			else
			{
				transform.position = new Vector3(transform.position.x, (float)(-0.464), transform.position.z);
				//hide blobshadow
				//GetComponent<MeshRenderer>().enabled = false;
			}
		}
	}

	//set shadow position
	void setPosition(RaycastHit hit)
	{
        //var temp = (hit.point + Vector3.up * Yoffset);
        //transform.position = new Vector3(temp.x, (float)(-0.464), temp.z);
        transform.position = (hit.point + Vector3.up * Yoffset);
    }

	//set shadow rotation to floor angle
	void setRotation(Vector3 normal)
	{
		transform.rotation = Quaternion.FromToRotation(Vector3.up, normal);
	}

	//set the scale of the blobshadow
	void setScale(float floorDistance)
	{
		float scaleMultiplier = floorDistance / DistanceScale;
		float size = BlobShadowSize + scaleMultiplier;
		transform.localScale = new Vector3(size, size, size);
	}
}

