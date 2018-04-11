using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
[RequireComponent (typeof(Waypoint))]
public class CubeEditor : MonoBehaviour {
	private Waypoint waypoint;

	private void Awake()
	{
		waypoint = GetComponent<Waypoint>();
	}

	void Update ()
	{
		if (!waypoint) { waypoint = GetComponent<Waypoint>(); }

		SnapToGrid();
		UpdateLabel();
	}

	private void SnapToGrid()
	{
		int gridSize = waypoint.GetGridSize();
		Vector2 gridPos = waypoint.GetGridPos();
		gridPos = gridPos* gridSize;

		transform.position = new Vector3( gridPos.x, 0, gridPos.y);
	}

	private void UpdateLabel()
	{
		TextMesh textMesh = GetComponentInChildren<TextMesh>();
		Vector2 gridPos = waypoint.GetGridPos();
		string labelText = gridPos.x + ", " + gridPos.y;
		textMesh.text = labelText;
		gameObject.name = "Cube " + labelText;
	}
}
