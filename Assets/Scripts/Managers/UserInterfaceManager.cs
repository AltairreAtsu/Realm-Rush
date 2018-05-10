using UnityEngine;
using UnityEngine.UI;

public class UserInterfaceManager : MonoBehaviour {

	[SerializeField] private Text scoreText;
	[SerializeField] private int maxScore;

	[SerializeField] private Text currencyText;
	[SerializeField] private int maxCurrency;

	[SerializeField] private Text healthText;
	[SerializeField] private Slider healthSlider;
	[SerializeField] private int maxDisplayedHealth;

	[SerializeField] private GameObject losePanel;
	[SerializeField] private GameObject winPanel;
	[SerializeField] private GameObject timeSliderPanel;

	[SerializeField] private Text turretLabel;
	[SerializeField] private Text repairLabel;
	[SerializeField] private Text UpgradeLabel;

	[SerializeField] private Text turretCapLabel;

	[SerializeField] private Text purchasedUpgradesLabel;
	[SerializeField] private int maxPurchasedUpgrades;

	private Slider[] sliders;

	void Start () {
		scoreText.text = "0";
		currencyText.text = "0";
		purchasedUpgradesLabel.text = "0";
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

	public void UpdateHealth(int health, int maxHealth)
	{
		if (SessionManager.IsGameLost()) { return; }

		UpdateDisplay(healthText, health, maxDisplayedHealth);
		healthSlider.value = (float)health / maxHealth;
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

	public void UpdatePurchasedUpgrades(int purchasedUpgrades)
	{
		UpdateDisplay(purchasedUpgradesLabel, purchasedUpgrades, maxPurchasedUpgrades);
	}

	public void UpdatePurchasedTowers(int usedTowers, int softCap)
	{
		turretCapLabel.text = usedTowers + "/" + softCap;
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
