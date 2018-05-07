using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ClosestEnemy
{
	public bool isInRange;
	public EnemyMovement enemy;
}

public class Tower : MonoBehaviour {
	[Tooltip("Attack range in grid cells.")]
	[SerializeField] private int attackRange = 5;
	[SerializeField] private int damage = 1;
	[SerializeField] private Transform turretTop;
	[SerializeField] private ParticleSystem bulletSystem;

	private ClosestEnemy closestEnemy = new ClosestEnemy();
	public Waypoint waypoint;

	private void Update ()
	{
		FindClosestEnemy();
		TargetEnemy();
		UpdateBulletSystem();
	}

	private void FindClosestEnemy()
	{
		var enemies = FindObjectsOfType<EnemyMovement>();
		if (enemies.Length == 0) { return; }

		foreach (EnemyMovement enemy in enemies)
		{
			float distance = Vector3.Distance(enemy.transform.position, transform.position);
			float closestEnemyDistance = 0f;
			if (closestEnemy.enemy)
			{
				closestEnemyDistance = Vector3.Distance(closestEnemy.enemy.transform.position, transform.position); // TODO: only calculate this when it's required
				closestEnemy.isInRange = closestEnemyDistance <= attackRange * Waypoint.GetGridSize();
			}

			if (!closestEnemy.enemy || distance < closestEnemyDistance)
			{
				closestEnemy.enemy = enemy;
			}
		}
	}

	private void TargetEnemy()
	{
		if (closestEnemy.enemy)
		{
			turretTop.LookAt(closestEnemy.enemy.transform);
		}	
	}

	private void UpdateBulletSystem()
	{
		if (!closestEnemy.enemy || !closestEnemy.isInRange)
		{
			if (bulletSystem.isPlaying) { bulletSystem.Stop(); }
		}

		if (closestEnemy.enemy && closestEnemy.isInRange)
		{
			if (!bulletSystem.isPlaying) { bulletSystem.Play(); }
		}
	}

	public int GetDamage()
	{
		return damage;
	}
}
