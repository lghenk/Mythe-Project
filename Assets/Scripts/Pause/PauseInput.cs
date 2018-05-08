using UnityEngine;

/// <summary>
/// Made by Koen Sparreboom
/// </summary>
public class PauseInput : MonoBehaviour {
    private void Update() {
        if (!Input.GetKeyDown(KeyCode.Escape))
            return;
        
        Pause.TogglePauseGame();
    }
}