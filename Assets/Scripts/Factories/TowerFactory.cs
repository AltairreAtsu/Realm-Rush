using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour {
	[SerializeField] private Tower towerPrefab = null;
	[SerializeField] private int towerSoftLimit = 5;
	[SerializeField] private int towerHardLimit = 10;
	[SerializeField] private UserInterfaceManager uiManager;

	private Queue<Tower> towerQueue = new Queue<Tower>();

	private void Start()
	{
		uiManager.UpdatePurchasedTowers(0, towerSoftLimit);
	}

	public void AddTower (Waypoint waypointBase)
	{
		if (towerQueue.Count < towerSoftLimit)
		{
			InstantiateNewTower(waypointBase);
			uiManager.UpdatePurchasedTowers(towerQueue.Count, towerSoftLimit);
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

	public bool IncreaseTowerSoftLimit(int amount)
	{
		if (towerSoftLimit <= towerHardLimit && towerSoftLimit + amount <= towerHardLimit)
		{
			towerSoftLimit += amount;
			uiManager.UpdatePurchasedTowers(towerQueue.Count, towerSoftLimit);
			return true;
		}

		return false;
	}
}
