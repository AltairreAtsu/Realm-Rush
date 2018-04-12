using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ClosestEnemy
{
	public float distance;
	public bool isInRange;
	public EnemyMovement enemy;
}

public class Tower : MonoBehaviour {
	[SerializeField]
	private Transform turretTop;

	[SerializeField]
	[Tooltip("Attack range in grid cells.")]
	private int attackRange = 5;

	[SerializeField]
	private ParticleSystem bulletSystem;

	private ClosestEnemy closestEnemy = new ClosestEnemy();

	void Update ()
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

			if (!closestEnemy.enemy || distance < closestEnemy.distance)
			{
				closestEnemy.enemy = enemy;
				closestEnemy.distance = distance;
				closestEnemy.isInRange = distance <= attackRange * Waypoint.GetGridSize();
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
}
