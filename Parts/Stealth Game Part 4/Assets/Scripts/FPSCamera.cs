using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCamera : MonoBehaviour {

	public bool justOffset;
	public bool drawGizmos;

	[Header("Player Options")]
	public Transform playerTransform;

	[Header("Camera Options")]
	public Vector3 cameraOffset;
	public float followSpeed;
	public float lookSpeed;

	[Header("Look Rotation")]
	public float xSpeed;
	public float ySpeed;

	public Vector2 xClamp;
	public Vector2 yClamp;

	float mouseX;
	float mouseY;
	private void LateUpdate() {
		
		Vector3 p = playerTransform.TransformPoint (cameraOffset);
		if (justOffset)
			p = playerTransform.position + cameraOffset;
		//transform.position = Vector3.Lerp (transform.position, p, followSpeed*Time.deltaTime);
		transform.position = p;
		if (!justOffset) {
			mouseX += Input.GetAxis ("Mouse X") * xSpeed;
			mouseY += Input.GetAxis ("Mouse Y") * ySpeed;
			mouseY = Mathf.Clamp (mouseY, -90, 90);

			transform.localRotation = Quaternion.Lerp(transform.localRotation,Quaternion.AngleAxis (mouseX, Vector3.up), lookSpeed*Time.deltaTime);
			transform.localRotation =  Quaternion.Lerp(transform.localRotation,transform.localRotation*Quaternion.AngleAxis (mouseY, Vector3.left), lookSpeed*Time.deltaTime);

			playerTransform.localRotation = Quaternion.AngleAxis (mouseX, Vector3.up);
		}
	}

	private void OnDrawGizmos() {
		if (drawGizmos) {
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere (playerTransform.TransformPoint (cameraOffset), 0.2f);
		}
	}

	float clamp (float val, float max, float min) {
		if (val > max)
			return max;
		if (val < min)
			return min;

		return val;

	}

}
