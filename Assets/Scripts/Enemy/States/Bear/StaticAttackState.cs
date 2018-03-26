using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Created by Timo Heijne
public class StaticAttackState : State {
	[HideInInspector]
	public Transform target;

	private void Start() {
		stateName = "StaticAttackState";
	}

	public override void Act(StateMachine machine) {
		throw new System.NotImplementedException();
	}

	public override void Reason(StateMachine machine) {
		throw new System.NotImplementedException();
	}
}
