using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary author="Koen Sparreboom">
/// Simple menu button functions
/// </summary>
public class ButtonFunctions : MonoBehaviour {
	public void LoadLevel(int level) {
		SceneManager.LoadScene(level);
	}

	public void LoadLevel(string level) {
		SceneManager.LoadScene(level);
	}

	public void QuitGame() {
		Application.Quit();
	}
}