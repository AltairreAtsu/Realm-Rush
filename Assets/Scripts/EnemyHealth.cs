using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {
	[SerializeField] private int hitPoints = 10;

	private bool dying = false;

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
			Die();
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
		explosionObjectPool.SpawnExplosion(transform.position);
		Destroy(gameObject); // TODO: sound effect and particle system for enemy deaths?
	}

	public void TriggerDeath()
	{
		if (!dying)
		{
			Die();
		}
	}

}
