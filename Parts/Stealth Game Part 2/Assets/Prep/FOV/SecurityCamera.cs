using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCamera : MonoBehaviour {

	public FOVManager FOV;
	public Transform endBone;
	public Vector3 offset;


	private void LateUpdate() {

		endBone.LookAt (FOV.hitPoint, Vector3.up);
		endBone.Rotate (offset);

	}

}
