using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class NodeEditor : EditorWindow {

	#region Setup
	[MenuItem("Editors/Node Editor")]
	private static void Init() {
		EditorWindow w = EditorWindow.GetWindow (typeof(NodeEditor), true, "Node Editor");
		w.Show ();
	}
	#endregion

	public List<Node> nodes = new List<Node>();

	private void OnGUI() {
		SetupHeader ();
		NodeLoop ();
		Repaint ();
	}


	#region MainGUI
	private void SetupHeader() {

		Rect header = r (0, 0, width, 30);
		GUI.Box (header, "");

		Rect placeNode = o (header, 20, 5, 100, 20);
		if (GUI.Button (placeNode, "Place Node")) {
			nodes.Add (new FOVNode());
		}

		Rect clearNodes = o (placeNode, 120, 0, 100, 20);

		if (GUI.Button (clearNodes, "Clear Nodes")) {
			nodes.Clear ();
		}

	}
	#endregion

	#region Node Handling

	private void NodeLoop() {
		for (int i = 0; i < nodes.Count; i++) {
			nodes [i].DrawNode ();
			DragNode (i);
		}
	}


	Node currentlyDragging = new Node();
	bool isDraggingNode = false;

	private void DragNode(int i) {
		Event e = Event.current;

		if (e.button == 0 && e.type == EventType.MouseDrag && !isDraggingNode) {
			if (nodes [i].pos.Contains (e.mousePosition)) {
				currentlyDragging = nodes [i];
				isDraggingNode = true;
			}
		}


		if (isDraggingNode) {

			currentlyDragging.pos.position = Vector2.Lerp(currentlyDragging.pos.position,new Vector2 (e.mousePosition.x - currentlyDragging.pos.size.x / 2, e.mousePosition.y - currentlyDragging.pos.size.y / 2),0.9f*Time.deltaTime);

		}


		if (e.button == 0 && e.type == EventType.MouseUp && isDraggingNode) {
			isDraggingNode = false;
		}

	}

	#endregion



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
