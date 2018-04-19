using UnityEngine;

/// <summary>
/// Made by Koen Sparreboom 8===============================D~~~~~~~~~~~~~
/// </summary>
public class Pause : MonoBehaviour {
    private static bool _paused;
    public static bool IsPaused => _paused;

    public static void PauseGame() {
        _paused = true;
        Time.timeScale = 0;

        EventManager.TriggerEvent("OnPauseChange");
    }

    public static void UnpauseGame() {
        _paused = false;
        Time.timeScale = 1;

        EventManager.TriggerEvent("OnPauseChange");
    }
}