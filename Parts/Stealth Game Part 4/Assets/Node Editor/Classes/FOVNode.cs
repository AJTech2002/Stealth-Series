using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class FOVNode : Node {

	private Object checker;
	private Object targetObject;

	private float angle;
	private float radius;

	public override void DrawNode() {
		pos.size = new Vector2 (300, 120);
		DrawBox ();
		DrawFOVNode ();
		CallAction ();
	}

	private void DrawBox() {
		GUI.Box (pos, "");
	}

	private void DrawFOVNode() {

		Rect name = o (pos, 10, 10, pos.width - 20, 20);
		Rect mainField = o (name, 0, 20, pos.width - 20, 20);
		Rect targetField = o (mainField, 0, 20, pos.width - 20, 20);

		Rect angleField = o (targetField, 0, 20, pos.width - 20, 20);
		Rect radiusField = o (angleField, 0, 20, pos.width - 20, 20);

		GUI.Label (name, "FOV NODE");
		checker = (Transform)EditorGUI.ObjectField (mainField, "Checker : ", checker, typeof(Transform), true);
		targetObject = (Transform)EditorGUI.ObjectField (targetField, "Target : ", targetObject, typeof(Transform), true);

		angle = EditorGUI.Slider (angleField, "Angle : ", angle, 0, 360);
		radius = EditorGUI.FloatField (radiusField, "Radius : ", radius);


	}

	public void CallAction() {
		if (checker != null && targetObject != null) {
			bool isInFov = FOVDetection.inFOV ((Transform)checker, (Transform)targetObject, angle, radius);
			Debug.Log (targetObject.name + " is in FOV of " + checker.name + " = " + isInFov);
		}
	}


	#region Helpers

	private float width {
		get {
			return Screen.width;
		}
	}

	private float height {
		get {
			return Screen.height;
		}
	}

	private Rect r (float xPos, float yPos, float xSize, float ySize) {
		return new Rect (new Vector2 (xPos, yPos), new Vector2 (xSize, ySize));
	}

	private Rect o (Rect r, float xOff, float yOff, float xSize, float ySize) {
		return new Rect (new Vector2 (r.position.x + xOff, r.position.y + yOff), new Vector2 (xSize, ySize));
	}


	#endregion

}
