using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Created By Timo Heijne
public class DecideAttackState : State {

	private FieldOfView _fieldOfView;

	[SerializeField] private float _staticRadius = 5;

	private void Start() {
		stateName = "DecideAttackState";
	}

	public override void EnterState(StateMachine machine) {
		_fieldOfView = GetComponent<FieldOfView>();
	}

	public override void Act(StateMachine machine) { }

	public override void Reason(StateMachine machine) {
		if (_fieldOfView.HasPlayerInRange()) {
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
