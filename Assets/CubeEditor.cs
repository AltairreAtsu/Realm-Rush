using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
public class CubeEditor : MonoBehaviour {

	[SerializeField]
	[Range (10, 20)][Tooltip ("The Size of the grid squares in the world.")]
	private int gridSize = 10;

	private TextMesh textMesh;

	private void Start()
	{
		textMesh = GetComponentInChildren<TextMesh>();
	}

	void Update () {
		if (!textMesh)
		{
			textMesh = GetComponentInChildren<TextMesh>();
		}

		Vector3 SnapPos;

		SnapPos.x = Mathf.RoundToInt(transform.position.x / gridSize) * gridSize;
		SnapPos.z = Mathf.RoundToInt(transform.position.z / gridSize) * gridSize;
		SnapPos.y = 0f;

		transform.position = SnapPos;
		textMesh.text = SnapPos.x/gridSize + ", " + SnapPos.z/gridSize;
	}
}
