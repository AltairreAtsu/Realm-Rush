using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
	private Pathfinder pathfinder;

	[SerializeField]
	private int hits = 10;

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
			yield return new WaitForSeconds(1f);
		}
	}

	private void OnParticleCollision(GameObject other)
	{
		Damage();
		if(hits <= 0)
		{
			Kill();
		}
	}

	private void Damage()
	{
		hits--;
		// TODO: Hit Sounds and Particles
	}

	private void Kill()
	{
		Destroy(gameObject); // TODO: sound effect and particle system for enemy deaths?
	}
}
