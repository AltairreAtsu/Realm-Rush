using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {
	[SerializeField] EnemySpawner enemyFactory;
	[SerializeField] UserInterfaceManager uiManager;

	private int score = 0;

	private void Start()
	{
		enemyFactory.EnemySpawnObservers += OnEnemySpawn;
	}

	private void OnEnemySpawn (EnemyHealth health) {
		health.EnemyDeathObservers += OnEnemyDeath;
	}

	private void OnEnemyDeath(int scoreValue, int currenyValue)
	{
		score += scoreValue;
		uiManager.UpdateScore(score);
	}
}
