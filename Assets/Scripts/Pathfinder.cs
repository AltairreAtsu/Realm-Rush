using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour {
	private Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
	private Queue<Waypoint> queue = new Queue<Waypoint>();
	private List<Waypoint> path = new List<Waypoint>();
	private Vector2Int[] dirrections =
	{
		Vector2Int.up,
		Vector2Int.right,
		Vector2Int.down,
		Vector2Int.left
	};
	private Waypoint searchCenter;

	[SerializeField]
	private Waypoint startWaypoint, endWaypoint;

	private void LoadBlocks()
	{
		var waypoints = FindObjectsOfType<Waypoint>();
		foreach (Waypoint waypoint in waypoints)
		{
			var gridPos = waypoint.GetGridPos();

			if (grid.ContainsKey(gridPos))
			{
				Debug.LogWarning("Duplicate cube at: " + gridPos + " ignoring!");
			}
			else
			{
				grid.Add(gridPos, waypoint);
			}
		}
	}

	private void BreadthFirstSearch()
	{
		queue.Enqueue(startWaypoint);
		
		while(queue.Count > 0)
		{
			searchCenter = queue.Dequeue();
			if(searchCenter == endWaypoint) { break; }
			ExploreNeighbours();
			searchCenter.isExplored = true;
		}
	}

	private void ExploreNeighbours()
	{
		foreach (Vector2Int dirrection in dirrections)
		{
			Vector2Int neighbourCoordinates = searchCenter.GetGridPos() + dirrection;
			if(grid.ContainsKey(neighbourCoordinates))
			{
				QueueNewNeighbours(neighbourCoordinates);
			}
		}
	}

	private void QueueNewNeighbours(Vector2Int nieghbourCoordinates)
	{
		Waypoint neighbour = grid[nieghbourCoordinates];
		if (!neighbour.isExplored && !queue.Contains(neighbour))
		{
			queue.Enqueue(neighbour);
			neighbour.exploredFrom = searchCenter;
		}
	}

	private void CreatePath()
	{
		path.Add(endWaypoint);

		Waypoint previous = endWaypoint.exploredFrom;
		while(previous != startWaypoint)
		{
			path.Add(previous);
			previous = previous.exploredFrom;
		}

		path.Add(startWaypoint);
		path.Reverse();
	}

	private void ColorStartAndEndPoint()
	{
		startWaypoint.SetTopColor(Color.red);
		endWaypoint.SetTopColor(Color.green);
	}

	public List<Waypoint> GetPath()
	{
		LoadBlocks();
		ColorStartAndEndPoint();
		BreadthFirstSearch();
		CreatePath();
		return path;
	}

	public Waypoint GetStartWaypoint()
	{
		return startWaypoint;
	}
	public Waypoint GetEndWaypoint()
	{
		return endWaypoint;
	}
}
