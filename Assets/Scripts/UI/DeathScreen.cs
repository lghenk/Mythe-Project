﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathScreen : MonoBehaviour {
	[SerializeField]
	private Image _image;

	[SerializeField]
	private Text _text;

	public int sceneIndex = 0;

	private void Update() {
		if (_image.gameObject.activeInHierarchy) {
			_image.color = Color.Lerp(_image.color, _image.color.ReturnNewColorThatsTheSameButWithDifferentAlphaDespacito(1), Time.unscaledDeltaTime * 5);
			_text.color = Color.Lerp(_text.color, _text.color.ReturnNewColorThatsTheSameButWithDifferentAlphaDespacito(1), Time.unscaledDeltaTime * 5);
			StartCoroutine(LoadMainMenu());
		}
	}

	IEnumerator LoadMainMenu() {
		yield return new WaitForSecondsRealtime(5);
		SceneManager.LoadScene(sceneIndex);
	}
}