﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {
	#region Variables
	[SerializeField] private int hitPoints = 10;
	[SerializeField] private int scoreValue = 10;
	[SerializeField] private int currencyValue = 1;

	private bool dying = false;

	public delegate void EnemyDeathEvent(int scoreValue, int currencyValue);
	public event EnemyDeathEvent EnemyDeathObservers;

	private static ExplosionObjectPool explosionObjectPool;
	#endregion

	private void Start()
	{
		if (explosionObjectPool == null)
		{
			explosionObjectPool = FindObjectOfType<ExplosionObjectPool>();
		}
	}

	private void OnParticleCollision(GameObject other)
	{
		Tower tower = other.GetComponentInParent<Tower>();

		Damage(tower.GetDamage());
		if (hitPoints <= 0)
		{
			TriggerDeath(true);
		}
	}

	private void Damage(int damage)
	{
		hitPoints -= damage;
		// TODO: Hit Sounds and Particles
	}

	private void Die(bool giveScore)
	{
		dying = true;
		if (giveScore)
		{
			if (EnemyDeathObservers != null) { EnemyDeathObservers(scoreValue, currencyValue); }
		}
		else
		{
			if (EnemyDeathObservers != null) { EnemyDeathObservers(0, 0); }
		}

		explosionObjectPool.SpawnExplosion(transform.position);
		Destroy(gameObject);
	}

	public void TriggerDeath(bool giveScore)
	{
		if (!dying)
		{
			Die(giveScore);
		}
	}

	#region Getters and Setters
	public void SetHealth(int hitPoints)
	{
		this.hitPoints = hitPoints;
	}

	public void SetScoreValue(int scoreValue)
	{
		this.scoreValue = scoreValue;
	}

	public void SetCurrencyValue (int currencyValue)
	{
		this.currencyValue = currencyValue;
	}
	#endregion
}
