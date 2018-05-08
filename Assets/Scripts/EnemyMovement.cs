using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(EnemyHealth))]
public class EnemyMovement : MonoBehaviour {
	private Pathfinder pathfinder;
	private EnemyHealth health;

	[SerializeField] private float movementPeriod = .5f;

	void Start ()
	{
		pathfinder = FindObjectOfType<Pathfinder>();
		health = GetComponent<EnemyHealth>();
		StartCoroutine(FollowPath(pathfinder.GetPath()));
	}

	private IEnumerator FollowPath(List<Waypoint> path)
	{
		foreach (Waypoint waypoint in path)
		{
			transform.position = waypoint.transform.position;
			yield return new WaitForSeconds(movementPeriod);
		}
		health.TriggerDeath(false);
	}


}
