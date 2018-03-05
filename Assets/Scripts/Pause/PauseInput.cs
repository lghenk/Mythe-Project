using UnityEngine;

/// <summary>
/// Made by Koen Sparreboom
/// </summary>
public class PauseInput : MonoBehaviour {
    [SerializeField]
    private GameObject _pauseUi;

    [SerializeField]
    private AudioClip _pause;

    [SerializeField]
    private AudioClip _unpause;

    private AudioSource _audioSource;

    private void Start() {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update() {
        if (!Input.GetKeyDown(KeyCode.Escape))
            return;

        if (!Pause.Paused) {
            Pause.PauseGame();
            _audioSource.PlayOneShot(_pause);
            _pauseUi.SetActive(true);
        } else {
            Pause.UnpauseGame();
            _audioSource.PlayOneShot(_unpause);
            _pauseUi.SetActive(false);
        }
    }
}