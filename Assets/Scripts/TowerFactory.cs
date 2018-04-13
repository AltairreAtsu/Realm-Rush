using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour {
	[SerializeField] private Tower towerPrefab = null;
	[SerializeField] private int towerLimit = 5;

	private Queue<Tower> towerQueue = new Queue<Tower>();

	public void AddTower (Waypoint waypointBase)
	{
		if (towerQueue.Count < towerLimit)
		{
			InstantiateNewTower(waypointBase);
		}
		else
		{
			MoveTower(waypointBase);
		}
	}

	private void InstantiateNewTower(Waypoint waypointBase)
	{
		var tower = Instantiate(towerPrefab, waypointBase.transform.position, Quaternion.identity);
		tower.waypoint = waypointBase;
		tower.transform.SetParent(gameObject.transform);
		waypointBase.isPlaceable = false;
		towerQueue.Enqueue(tower);
	}

	private void MoveTower(Waypoint waypointBase)
	{
		var tower = towerQueue.Dequeue();

		tower.transform.position = waypointBase.transform.position;

		waypointBase.isPlaceable = false;
		tower.waypoint.isPlaceable = true;
		tower.waypoint = waypointBase;

		towerQueue.Enqueue(tower);
	}
}
