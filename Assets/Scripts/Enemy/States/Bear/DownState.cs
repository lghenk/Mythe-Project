using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownState : State {

	[SerializeField, Tooltip("Time in seconds in which if the object is down will bleed out")] private float _bleedOutTime = 60;
	private float _downedTime;
	private Health _health;
	private StateMachine _machine;
	private AnimationHandler _animationHandler;
	private BehaviourState _behaviourState;

	// Use this for initialization
	void Start () {
		stateName = "DownState";

		_health = GetComponent<Health>();
		_health.onDeath += OnDeath;
		_health.onMessage += OnMessage;

		_machine = GetComponent<StateMachine>();
		_animationHandler = GetComponent<AnimationHandler>();

		_behaviourState = GetComponent<BehaviourState>();
	}

	private void OnMessage(string s) {
		if (s == "DBNO") { // The health system just announced that its now DBNO
			_machine.CurrentState = _machine.GetState(stateName); // Switch the state machine to this.
		} else if (s == "Resurrect") {
			_behaviourState.SetState(BehaviourState.BehaviourStates.Friendly);
			_machine.CurrentState = _machine.GetState("IdleState");
		}
	}

	public override void EnterState(StateMachine machine) {
		_animationHandler.SetAnimation("Dead", true);
		_downedTime = Time.timeSinceLevelLoad;
		_behaviourState.SetState(BehaviourState.BehaviourStates.DBNO);
	}

	public override void Act(StateMachine machine) {
		if (Time.timeSinceLevelLoad - _downedTime >= _bleedOutTime) {
			// Player let animal bleed out...
			// Trigger kill stuff for now Destroy
			Destroy(gameObject);
		}
	}

	public override void Reason(StateMachine machine) {
		//throw new System.NotImplementedException();
	}

	public override void ExitState(StateMachine machine) {
		_animationHandler.SetAnimation("Dead", false);
	}
	
	private void OnDeath(Health health) {
		Destroy(gameObject); // Player decided to kill this poor beast. Destroy it
	}
}
