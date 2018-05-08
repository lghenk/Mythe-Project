using UnityEngine;
using UnityEngine.UI;

public class FillBar : MonoBehaviour {

    private Image _image;

    private void Start() {
        _image = GetComponent<Image>();
    }

    public void SetValue(float max, float cur) {
        _image.fillAmount = cur / max;
    }
}