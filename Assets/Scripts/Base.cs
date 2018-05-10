using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour {
	[SerializeField] private UserInterfaceManager uiManager;
	[SerializeField] private ParticleSystem explosion;
	[SerializeField] private int health = 10;
	[SerializeField] private int maxHealth = 20;

	private AudioSource audioSource;
	private bool alive = true;

	public delegate void BaseDeathEvent();
	public event BaseDeathEvent BaseDeathObservers;

	private void Start()
	{
		uiManager.UpdateHealth(health, maxHealth);
		audioSource = GetComponent<AudioSource>();
	}

	private void OnTriggerEnter(Collider other)
	{
		health--;
		uiManager.UpdateHealth(health, maxHealth);

		if(health <= 0 && alive)
		{
			alive = false;
			if (BaseDeathObservers != null ) { BaseDeathObservers(); }
			explosion.Play();
		}
	}

	public bool Repair (int amount)
	{
		if(health != maxHealth)
		{
			health += amount;
			health = IntClmap(health, 0, maxHealth);

			uiManager.UpdateHealth(health, maxHealth);
			if (audioSource.isPlaying)
			{
				audioSource.Stop();
				audioSource.Play();
			}
			else
			{
				audioSource.Play();
			}
			return true;
		}
		return false;
	}

	private int IntClmap(int value, int min, int max)
	{
		if (value < min) { return min; }
		if (value > max) { return max; }
		return value;
	}
}
