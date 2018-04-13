using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
	private Pathfinder pathfinder;

	[SerializeField] private int hitPoints = 10;
	[SerializeField] private float movementPeriod = .5f;

	void Start ()
	{
		pathfinder = FindObjectOfType<Pathfinder>();
		StartCoroutine(FollowPath(pathfinder.GetPath()));
	}

	private IEnumerator FollowPath(List<Waypoint> path)
	{
		foreach (Waypoint waypoint in path)
		{
			transform.position = waypoint.transform.position;
			yield return new WaitForSeconds(movementPeriod);
		}
		Die();
	}

	private void OnParticleCollision(GameObject other)
	{
		Damage();
		if(hitPoints <= 0)
		{
			Die();
		}
	}

	private void Damage()
	{
		hitPoints--;
		// TODO: Hit Sounds and Particles
	}

	private void Die()
	{
		Destroy(gameObject); // TODO: sound effect and particle system for enemy deaths?
	}
}
