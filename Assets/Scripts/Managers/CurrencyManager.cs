using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : MonoBehaviour {
	[SerializeField] EnemySpawner enemyFactory;
	[SerializeField] TowerFactory towerFactory;
	[SerializeField] Base mainBase;
	[SerializeField] UserInterfaceManager uiManager;
	[SerializeField] Tower standardTower;
	[Header("Repair Fields")]
	[SerializeField] int repairCost;
	[SerializeField] int repairAmount;

	private int currency = 0;

	private void Start()
	{
		enemyFactory.EnemySpawnObservers += OnEnemySpawn;
	}

	private void OnEnemySpawn(EnemyHealth health)
	{
		health.EnemyDeathObservers += OnEnemyDeath;
	}

	private void OnEnemyDeath(int scoreValue, int currenyValue)
	{
		currency += currenyValue;
		uiManager.UpdateCurrnecy(currency);
	}

	private bool BuyTurret(Tower tower)
	{
		var cost = tower.GetCost();
		if (currency >= cost)
		{
			if (towerFactory.IncreaseTowerSoftLimit(1))
			{
				currency -= cost;
				uiManager.UpdateCurrnecy(currency);
				return true;
			}
		}

		return false;
	}

	public void BuyStandardTower()
	{
		BuyTurret(standardTower);
	}

	public void RepairBase()
	{
		if (currency >= repairCost)
		{
			currency -= repairCost;
			uiManager.UpdateCurrnecy(currency);
			mainBase.Repair(repairAmount);
		}
		
	}
}
