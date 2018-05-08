using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {
	[SerializeField] private int hitPoints = 10;
	[SerializeField] private int scoreValue = 10;
	[SerializeField] private int currencyValue = 1;

	private bool dying = false;

	public delegate void EnemyDeathEvent(int scoreValue, int currencyValue);
	public event EnemyDeathEvent EnemyDeathObservers;

	private static ExplosionObjectPool explosionObjectPool;

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
			TriggerDeath();
		}
	}

	private void Damage(int damage)
	{
		hitPoints -= damage;
		// TODO: Hit Sounds and Particles
	}

	private void Die()
	{
		dying = true;
		if(EnemyDeathObservers != null) { EnemyDeathObservers(scoreValue, currencyValue); }
		explosionObjectPool.SpawnExplosion(transform.position);
		Destroy(gameObject);
	}

	public void TriggerDeath()
	{
		if (!dying)
		{
			Die();
		}
	}

}
