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

	[SerializeField] private int costPerPurchaseIncriment = 1;
	private int additionalCostPerPurchase = 0;

	private int currency = 0;
	private int purchasedUpgrades = 0;

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

	private bool CanAfford(int cost)
	{
		if(currency >= cost + additionalCostPerPurchase)
		{
			return true;
		}
		return false;
	}

	private void TaxedPay (int cost)
	{
		Pay(cost);
		additionalCostPerPurchase += costPerPurchaseIncriment;

		uiManager.UpdateRepairCost(repairCost + additionalCostPerPurchase);
		uiManager.UpdateTurretCost(standardTower.GetCost() + additionalCostPerPurchase);
		uiManager.UpdateUpgradeCost(upgradeCost + additionalCostPerPurchase);
	}

	private void Pay (int cost)
	{
		currency -= cost + additionalCostPerPurchase;

	}

	private bool BuyTurret(Tower tower)
	{
		var cost = tower.GetCost();
		if (CanAfford(cost))
		{
			if (towerFactory.IncreaseTowerSoftLimit(1))
			{
				TaxedPay(cost);
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
		if (CanAfford(repairCost) && mainBase.Repair(repairAmount))
		{
			Pay(repairCost);
			uiManager.UpdateCurrnecy(currency);
		}
		
	}

	public void BuyTurretUpgrade()
	{
		if(CanAfford(upgradeCost))
		{
			TaxedPay(upgradeCost);
			uiManager.UpdateCurrnecy(currency);
			purchasedUpgrades++;
			uiManager.UpdatePurchasedUpgrades(purchasedUpgrades);

			var turrets = FindObjectsOfType<Tower>();
			foreach (Tower tower in turrets)
			{
				tower.IncreaseDamage(damageUpgrade);
			}
		}
	}
}
