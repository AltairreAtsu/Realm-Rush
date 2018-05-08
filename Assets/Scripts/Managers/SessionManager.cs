using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SessionManager : MonoBehaviour {
	[SerializeField] private UserInterfaceManager uiManager;
	[SerializeField] private AudioClip loseGameSound;
	[SerializeField] private float loseScreenLoadDelay = 0.5f;
	[SerializeField] private int endScreenBuildIndex = 1;

	private Base mainBase;
	private AudioSource audioSource;

	private static bool GAME_LOST = false;

	private void Start () {
		audioSource = GetComponent<AudioSource>();
		mainBase = FindObjectOfType<Base>();
		mainBase.BaseDeathObservers += OnBaseDeath;
	}
	
	private void OnBaseDeath () {
		GAME_LOST = true;
		audioSource.clip = loseGameSound;
		audioSource.Play();
		uiManager.DisplayLosePanel();
		StartCoroutine(LoadEndScreen());
	}

	private IEnumerator LoadEndScreen()
	{
		yield return new WaitForSeconds(loseScreenLoadDelay + loseGameSound.length);
		SceneManager.LoadScene(endScreenBuildIndex);
	}

	public static bool IsGameLost()
	{
		return GAME_LOST;
	}
}
