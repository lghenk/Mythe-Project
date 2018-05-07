using UnityEngine;
using UnityEngine.UI;

public class DeathScreen : MonoBehaviour {
	[SerializeField]
	private Image _image;

	[SerializeField]
	private Text _text;

	private void Update() {
		if (_image.gameObject.activeInHierarchy) {
			_image.color = Color.Lerp(_image.color, _image.color.GetNewAlpha(1), Time.unscaledDeltaTime * 5);
			_text.color = Color.Lerp(_text.color, _text.color.GetNewAlpha(1), Time.unscaledDeltaTime * 5);
		}
	}
}