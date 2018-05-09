using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {

	public void LoadNextLevel()
	{
		LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void LoadLevel(int buildIndex)
	{
		SceneManager.LoadScene(buildIndex);
	}

	public void Quit()
	{
		Application.Quit();
	}
}
