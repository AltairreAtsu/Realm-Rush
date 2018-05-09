using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyEngine : MonoBehaviour {
	[SerializeField] EnemySpawner enemyFactory;

	[SerializeField] int healthMin = 3;
	[SerializeField] int healthMax = 15;
	[SerializeField] float minSpeed = 0.7f;
	[SerializeField] float maxSpeed = 0.3f;
	[SerializeField] float increaseInterval = 30f;
	[SerializeField] int healthIncreasePerInterval = 2;
	
	public void UpdateDifficulty (float gameTime)
	{
		int bonusHealth = healthIncreasePerInterval * ( Mathf.FloorToInt(gameTime / increaseInterval) );
		int enemyHealth = healthMin + bonusHealth;
		enemyHealth = IntClmap(enemyHealth, healthMin, healthMax);

		enemyFactory.SetEnemySpawnHealth(enemyHealth);
		enemyFactory.SetEnemySpawnSpeed(Random.Range(minSpeed, maxSpeed) );
	}

	private int IntClmap(int value, int min, int max)
	{
		if (value < min) { return min; }
		if (value > max) { return max; }
		return value;
	}
}
