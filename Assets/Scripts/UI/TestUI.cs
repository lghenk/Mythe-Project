using UnityEngine;

public class TestUI : MonoBehaviour {
	[SerializeField]
	private PanelUI _panel;
	
	private void Update() {
		if (Input.GetKeyDown(KeyCode.A)) {
			_panel.Visible = !_panel.Visible;
		}
	}
}
