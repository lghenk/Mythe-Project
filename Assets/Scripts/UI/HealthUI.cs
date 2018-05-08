using UnityEngine;

public class HealthUI : MonoBehaviour {

	private Health _health;

	[SerializeField]
	private FillBar _fillBar;

	private void Start() {
		_health = GetComponent<Health>();
	}

	private void Update() {
		_fillBar.SetValue(_health.StartingHealth, _health.CurHealth);
	}
}
