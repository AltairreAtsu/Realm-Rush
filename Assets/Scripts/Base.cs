using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour {

	[SerializeField] private int health = 10;

	private void OnTriggerEnter(Collider other)
	{
		health--;
		if(health <= 0)
		{
			// Loose the game
		}
	}
}
