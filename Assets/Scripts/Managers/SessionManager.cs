using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SessionManager : MonoBehaviour {
	[SerializeField] private UserInterfaceManager uiManager;

	[Header ("Win Game Variables")]
	[SerializeField] private AudioClip winGameSound;
	[Tooltip ("Time it takes to win the game in minutes.")]
	[SerializeField] private float winTime = 3f;
	

	[Header ("Lose Game Variables")]
	[SerializeField] private AudioClip loseGameSound;
	[SerializeField] private float loseScreenLoadDelay = 0.5f;
	[SerializeField] private int endScreenBuildIndex = 1;

	private Base mainBase;
	private AudioSource audioSource;

	private float startTime;
	private float currentTime;

	private static bool GAME_LOST = false;

	private void Start () {
		audioSource = GetComponent<AudioSource>();
		mainBase = FindObjectOfType<Base>();
		mainBase.BaseDeathObservers += OnBaseDeath;

		ParseWinTime();
		startTime = Time.time;
	}

	private void Update()
	{
		currentTime = Time.time;
		var timePercent = 1 - (( currentTime - startTime ) / winTime) ;
		uiManager.UpdateTimeSliders(timePercent);

		if (currentTime - startTime > winTime)
		{
			// Win the game
			audioSource.clip = winGameSound;
			audioSource.Play();
			uiManager.DisplayWinPanel();
			StartCoroutine(LoadEndScreen());
		}
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
		yield return new WaitForSeconds(loseScreenLoadDelay + audioSource.clip.length);
		SceneManager.LoadScene(endScreenBuildIndex);
	}

	private void ParseWinTime()
	{
		winTime = winTime * 60;
	}

	public static bool IsGameLost()
	{
		return GAME_LOST;
	}
}
