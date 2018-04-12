using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour {
	private const int gridSize = 10;

	private Vector2Int gridpos;

	[HideInInspector]
	public bool isExplored = false;
	[HideInInspector]
	public Waypoint exploredFrom;

	[SerializeField]
	private Transform topPlane;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void SetTopColor(Color color)
	{
		topPlane.GetComponent<MeshRenderer>().material.color = color;
	}

	public Vector2Int GetGridPos()
	{
		return new Vector2Int(
					Mathf.RoundToInt(transform.position.x / gridSize),
					Mathf.RoundToInt(transform.position.z / gridSize)
					);
	}

	public static int GetGridSize()
	{
		return gridSize;
	}
}
