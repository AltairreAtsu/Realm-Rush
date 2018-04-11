using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

	private Pathfinder pathfinder;

	// Use this for initialization
	void Start ()
	{
		pathfinder = FindObjectOfType<Pathfinder>();
		StartCoroutine(FollowPath(pathfinder.GetPath()));
	}

	private IEnumerator FollowPath(List<Waypoint> path)
	{
		foreach (Waypoint waypoint in path)
		{
			print("running!");
			transform.position = waypoint.transform.position;
			yield return new WaitForSeconds(1f);
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
