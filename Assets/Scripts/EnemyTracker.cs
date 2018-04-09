using System.Collections.Generic;
using UnityEngine;

public class EnemyTracker : MonoBehaviour {

	public static List<EnemyTracker> activeEnemies;

	private void Awake() {
		if (activeEnemies == null) {
			activeEnemies = new List<EnemyTracker>();
		}
	}

	private void OnEnable() {
		activeEnemies.Add(this);
	}

	private void OnDisable() {
		activeEnemies.Remove(this);
	}
}
