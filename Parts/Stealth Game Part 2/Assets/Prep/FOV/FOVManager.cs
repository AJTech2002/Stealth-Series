using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOVManager : MonoBehaviour {

	//Raycast and then sphere cast...
	public float viewRadius;
	public LayerMask playerMask;

	public bool playerIsInView = false;

	public Transform player;

	private void Update() {
		CheckForward ();
	}

	
	private void CheckForward() 
	{
		Ray ray = new Ray (transform.position, transform.forward);

		if (playerIsInView)
			ray = new Ray (transform.position, player.position - transform.position);

		RaycastHit hit;

		if (Physics.Raycast (ray, out hit, 50)) {
			playerIsInView = playerIsInFOV (hit);
		}



	}

	[HideInInspector]
	public Vector3 hitPoint = Vector3.zero;
	public bool playerIsInFOV(RaycastHit hit) {

		Vector3 p = hit.point;
		bool didHit = Physics.CheckSphere (p, viewRadius, playerMask, QueryTriggerInteraction.UseGlobal);
		
		hitPoint = p;

		return didHit;

	}

	private void OnDrawGizmos() {
		if (!playerIsInView)
			Gizmos.color = Color.yellow;
		else
			Gizmos.color = Color.green;

		if (hitPoint == Vector3.zero)
			Gizmos.DrawWireSphere (transform.position, viewRadius);
		else {
			Gizmos.DrawLine (transform.position, hitPoint);
			Gizmos.DrawWireSphere (hitPoint, viewRadius);
		}
	}

}
