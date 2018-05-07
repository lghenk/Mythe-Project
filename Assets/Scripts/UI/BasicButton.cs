using UnityEngine;
using UnityEngine.SceneManagement;

public class BasicButton : MonoBehaviour {
    public void LoadLevel(int levelIndex) {
        SceneManager.LoadScene(levelIndex);
    }

    public void LoadLevel(string levelName) {
        SceneManager.LoadScene(levelName);
    }

    public void QuitApplication() {
        Application.Quit();
    }
}