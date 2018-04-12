using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
	[Tooltip ("Time in seconds between enemy spawns.")]
	[SerializeField] private float timeBetweenSpawns = 2f;
	[Tooltip ("The Enemy Prefab to spawn.")]
	[SerializeField] private EnemyMovement enemyPrefab;
	[SerializeField] private Transform spawnLocation;

	void Start () {
		if (enemyPrefab && spawnLocation)
		{
			StartCoroutine(SpawnEnemy());
		}
		else
		{
			Debug.LogWarning("EnemyPrefab and or Spawn locaiton cannot be null!");
		}
	}
	
	private IEnumerator SpawnEnemy()
	{
		while (true) {
			var enemy = Instantiate(enemyPrefab, spawnLocation.position, spawnLocation.rotation);
			enemy.transform.SetParent(transform);
			yield return new WaitForSeconds(timeBetweenSpawns);
		}
	}
}
