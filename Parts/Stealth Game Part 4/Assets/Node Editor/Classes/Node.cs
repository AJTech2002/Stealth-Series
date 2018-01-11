using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Node {

	public Rect pos = new Rect (new Vector2 (100, 100), new Vector2 (100, 100));
	public string name;

	public virtual void DrawNode () {
		DrawBox ();
	}

	private void DrawBox() {
		GUI.Box (pos, name);
	}



}
