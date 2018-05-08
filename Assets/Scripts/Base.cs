using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour {
	[SerializeField] private UserInterfaceManager uiManager;
	[SerializeField] private ParticleSystem explosion;
	[SerializeField] private int health = 10;

	private AudioSource audioSource;
	private bool alive = true;

	public delegate void BaseDeathEvent();
	public event BaseDeathEvent BaseDeathObservers;

	private void Start()
	{
		uiManager.UpdateHealth(health);
		audioSource = GetComponent<AudioSource>();
	}

	private void OnTriggerEnter(Collider other)
	{
		health--;
		uiManager.UpdateHealth(health);

		if(health <= 0 && alive)
		{
			alive = false;
			if (BaseDeathObservers != null ) { BaseDeathObservers(); }
			explosion.Play();
		}
	}

	public void Repair (int amount)
	{
		health += amount;
		uiManager.UpdateHealth(health);
		if (!audioSource.isPlaying)
		{
			audioSource.Play();
		}
	}
}
