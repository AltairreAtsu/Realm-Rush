using UnityEngine;
using UnityEngine.UI;

public class UserInterfaceManager : MonoBehaviour {

	[SerializeField] private Text scoreText;
	[SerializeField] private int maxScore;

	[SerializeField] private Text currencyText;
	[SerializeField] private int maxCurrency;

	[SerializeField] private Text healthText;
	[SerializeField] private int maxHealth;

	[SerializeField] private GameObject losePanel;
	[SerializeField] private GameObject winPanel;
	[SerializeField] private GameObject timeSliderPanel;

	[SerializeField] Text turretLabel;
	[SerializeField] Text repairLabel;
	[SerializeField] Text UpgradeLabel;

	private Slider[] sliders;

	void Start () {
		scoreText.text = "0";
		currencyText.text = "0";
		sliders = timeSliderPanel.GetComponentsInChildren<Slider>();
	}

	public void UpdateScore(int score)
	{
		if (SessionManager.IsGameLost()) { return;  }
		UpdateDisplay(scoreText, score, maxScore);
	}

	public void UpdateCurrnecy(int currency)
	{
		if (SessionManager.IsGameLost()) { return; }
		UpdateDisplay(currencyText, currency, maxCurrency);
	}

	public void UpdateTurretCost(int cost)
	{
		UpdateCostString(turretLabel, cost);
	}

	public void UpdateRepairCost(int cost)
	{
		UpdateCostString(repairLabel, cost);
	}

	public void UpdateUpgradeCost(int cost)
	{
		UpdateCostString(UpgradeLabel, cost);
	}

	public void UpdateHealth(int health)
	{
		if (SessionManager.IsGameLost()) { return; }
		UpdateDisplay(healthText, health, maxHealth);
	}

	public void UpdateTimeSliders(float percent)
	{
		foreach (Slider slider in sliders)
		{
			slider.value = percent;
		}
	}

	public void DisplayLosePanel()
	{
		losePanel.SetActive(true);
	}

	public void DisplayWinPanel()
	{
		winPanel.SetActive(true);
	}

	private void UpdateCostString(Text text, int cost)
	{
		text.text = cost + " coins";
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
