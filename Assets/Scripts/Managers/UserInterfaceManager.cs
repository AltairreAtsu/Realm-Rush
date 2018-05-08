using UnityEngine;
using UnityEngine.UI;

public class UserInterfaceManager : MonoBehaviour {

	[SerializeField] private Text scoreText;
	[SerializeField] private int maxScore;

	[SerializeField] private Text healthText;
	[SerializeField] private int maxHealth;

	[SerializeField] private GameObject losePanel;

	void Start () {
		scoreText.text = "0";
	}

	public void UpdateScore(int score)
	{
		if (SessionManager.IsGameLost()) { return;  }
		UpdateDisplay(scoreText, score, maxScore);
	}

	public void UpdateHealth(int health)
	{
		if (SessionManager.IsGameLost()) { return; }
		UpdateDisplay(healthText, health, maxHealth);
	}

	public void DisplayLosePanel()
	{
		losePanel.SetActive(true);
	}

	private void UpdateDisplay(Text text, int value, int max)
	{
		value = IntClmap(value, 0, max);
		text.text = value.ToString();
	}

	private int IntClmap(int value, int min, int max)
	{
		if(value < min) { return min; }
		if(value > max) { return max; }
		return value;
	}
}
