using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Created By Timo Heijne
public class DecideAttackState : State {

	private FieldOfView _fieldOfView;

	[SerializeField] private float _staticRadius = 3;
	private BehaviourState _behaviourState;
	private StateMachine _stateMachine;
	

	private void Start() {
		stateName = "DecideAttackState";
		_stateMachine = GetComponent<StateMachine>();
		_fieldOfView = GetComponent<FieldOfView>();
		_behaviourState = GetComponent<BehaviourState>();

		_fieldOfView.onDetectPlayer += OnDetectPlayer;
	}

	private void OnDetectPlayer(Transform transform) {
		if (_behaviourState.State == BehaviourState.BehaviourStates.Hostile &&
		    _stateMachine.CurrentState.stateName != "StaticAttackState" &&
		    _stateMachine.CurrentState.stateName != "ChargeAttackState") 
		{		
			_stateMachine.CurrentState = _stateMachine.GetState(stateName);
		}
	}

	public override void EnterState(StateMachine machine) { }

	public override void Act(StateMachine machine) { }

	public override void Reason(StateMachine machine) {		
		if (_fieldOfView.HasPlayerInRange() && _behaviourState.State == BehaviourState.BehaviourStates.Hostile) {
			if (Vector3.Distance(_fieldOfView.ItemsInView[0].position, transform.position) <= _staticRadius) {
				StaticAttackState sas = (StaticAttackState)machine.GetState("StaticAttackState");
				sas.target = _fieldOfView.ItemsInView[0];
				
				machine.CurrentState = machine.GetState("StaticAttackState");
			} else {					
				ChargeAttackState cas = (ChargeAttackState)machine.GetState("ChargeAttackState");
				cas.target = _fieldOfView.ItemsInView[0];
				
				machine.CurrentState = machine.GetState("ChargeAttackState");				
			}
		} else {
			machine.CurrentState = machine.GetState("IdleState");
		}
	}
}
