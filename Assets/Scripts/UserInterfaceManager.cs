using UnityEngine;
using UnityEngine.UI;

public class UserInterfaceManager : MonoBehaviour {

	[SerializeField] private Text scoreText;
	[SerializeField] private int maxScore;

	void Start () {
		scoreText.text = string.Empty;
	}

	public void UpdateScore(int score)
	{
		// Update Score Display
		score = IntClmap(score, 0, maxScore);
		scoreText.text = score.ToString();
	}

	private int IntClmap(int value, int min, int max)
	{
		if(value < min) { return min; }
		if(value > max) { return max; }
		return value;
	}
}
