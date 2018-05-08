using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathEvent : MonoBehaviour {

	private Health _health;

	[SerializeField] private GameObject _deathScreen;
	
	// Use this for initialization
	void Start () {
		_health = GetComponent<Health>();
		_health.onDeath += OnDeath;
	}

	private void OnDeath(Health health) {
		_deathScreen.SetActive(true);
	}
}
