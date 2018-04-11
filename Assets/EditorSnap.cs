using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class EditorSnap : MonoBehaviour {

	[SerializeField]
	[Range (10, 20)][Tooltip ("The Size of the grid squares in the world.")]
	private int gridSize = 10;

	void Update () {
		Vector3 SnapPos;
		SnapPos.x = Mathf.RoundToInt(transform.position.x / gridSize) * gridSize;
		SnapPos.z = Mathf.RoundToInt(transform.position.z / gridSize) * gridSize;
		SnapPos.y = 0f;

		transform.position = SnapPos;
	}
}
