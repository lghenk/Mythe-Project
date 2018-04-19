using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownState : State {

	[SerializeField, Tooltip("Time in seconds in which if the object is down will bleed out")] private float _bleedOutTime = 60;
	private float _downedTime;
	private Health _health;
	private StateMachine _machine;

	// Use this for initialization
	void Start () {
		stateName = "DownState";

		_health = GetComponent<Health>();
		_health.onDeath += OnDeath;
		_health.onMessage += OnMessage;

		_machine = GetComponent<StateMachine>();
	}

	private void OnMessage(string s) {
		if (s == "DBNO") { // The health system just announced that its now DBNO
			_machine.CurrentState = _machine.GetState(stateName); // Switch the state machine to this.
		}
	}

	public override void EnterState(StateMachine machine) {
		_downedTime = Time.timeSinceLevelLoad;
	}

	public override void Act(StateMachine machine) {
		throw new System.NotImplementedException();
	}

	public override void Reason(StateMachine machine) {
		throw new System.NotImplementedException();
	}

	public override void ExitState(StateMachine machine) {
		base.ExitState(machine);
	}
	
	private void OnDeath(Health health) {
		throw new NotImplementedException();
	}
}
