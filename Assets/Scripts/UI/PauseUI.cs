using UnityEngine;

public class PauseUI : MonoBehaviour
{

	[SerializeField] private PanelUI _panel;
	
	private void Start()
	{
		EventManager.StartListening("OnPauseChange", OnPauseChange);
	}

	private void OnPauseChange()
	{
		_panel.Visible = Pause.IsPaused;
	}
}
