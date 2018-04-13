using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour {
	[HideInInspector] public bool isExplored = false;
	[HideInInspector] public bool isPlaceable = true;
	[HideInInspector] public Waypoint exploredFrom;

	[SerializeField] private Transform topPlane;

	private const int gridSize = 10;
	private Vector2Int gridpos;
	private TowerFactory towerFactory = null;

	private void Start()
	{
		towerFactory = FindObjectOfType<TowerFactory>();
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

	private void OnMouseDown()
	{
		if(isPlaceable)
		{
			towerFactory.AddTower(this);
		}
		else
		{
			print("Can't place here!");
		}
	}
}
