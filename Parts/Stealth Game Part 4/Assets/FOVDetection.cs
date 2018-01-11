using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOVDetection : MonoBehaviour {

	public Transform player;
	public float angle;
	public float radius;


	private bool isInFov = false;

	private void OnDrawGizmos() {

		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere (transform.position, radius);

		Vector3 fovLine1 = Quaternion.AngleAxis (angle, transform.up) * transform.forward * radius;
		Vector3 fovLine2 = Quaternion.AngleAxis (-angle, transform.up) * transform.forward * radius;


		Gizmos.color = Color.blue;
		Gizmos.DrawRay (transform.position, fovLine1);
		Gizmos.DrawRay (transform.position, fovLine2);

		if (!isInFov)
			Gizmos.color = Color.red;
		else if (isInFov)
			Gizmos.color = Color.green;
		Gizmos.DrawRay (transform.position, (player.position - transform.position).normalized * radius);
		Gizmos.color = Color.black;
		Gizmos.DrawRay (transform.position, transform.forward * radius);

	}

	private void Update() {

		isInFov = inFOV (transform, player, angle, radius);

	}


	public static bool inFOV (Transform checkingObject, Transform target, float maxAngle, float maxRadius) {

		Collider[] overlaps = new Collider[10];

		int count = Physics.OverlapSphereNonAlloc (checkingObject.position, maxRadius, overlaps);

		for (int i = 0; i < count + 1; i++) {

			if (overlaps [i] != null) {

				if (overlaps [i].transform == target.transform) {

					Vector3 dir = (target.position - checkingObject.position).normalized;
					dir.y *= 0; 
					float angle = Vector3.Angle (checkingObject.forward, dir);

					if (angle <= maxAngle) {

						Ray ray = new Ray (checkingObject.position, target.position - checkingObject.position);
						RaycastHit hit;

						if (Physics.Raycast (ray, out hit, maxRadius)) {

							if (hit.transform == target)
								return true;

						}


					}


				}


			}

		}

		return false;

	}

}
