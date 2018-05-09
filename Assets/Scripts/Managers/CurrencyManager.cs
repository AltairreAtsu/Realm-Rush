using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : MonoBehaviour {
	[SerializeField] private EnemySpawner enemyFactory;
	[SerializeField] private TowerFactory towerFactory;
	[SerializeField] private Base mainBase;
	[SerializeField] private UserInterfaceManager uiManager;
	[SerializeField] private Tower standardTower;
	[Header("Repair Fields")]
	[SerializeField] private int repairCost = 3;
	[SerializeField] private int repairAmount = 1;
	[Header("Turret Upgrade Fields")]
	[SerializeField] private int upgradeCost = 3;
	[SerializeField] private int damageUpgrade = 1;
	
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

	public void BuyTurretUpgrade()
	{
		if(currency >= upgradeCost)
		{
			currency -= upgradeCost;
			uiManager.UpdateCurrnecy(currency);

			var turrets = FindObjectsOfType<Tower>();
			foreach (Tower tower in turrets)
			{
				tower.IncreaseDamage(damageUpgrade);
			}
		}
			}
}
