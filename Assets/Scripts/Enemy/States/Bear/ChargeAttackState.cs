using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Created by Timo Heijne
public class ChargeAttackState : State {
	[HideInInspector]
	public Transform target;
	
	private NavMeshAgent _navMeshAgent;
 
	private void Start() {
		stateName = "ChargeAttackState";
		_navMeshAgent = GetComponent<NavMeshAgent>();
	}

	public override void EnterState(StateMachine machine) {
		_navMeshAgent.speed = 5;
	}

	public override void Act(StateMachine machine) {
		_navMeshAgent.SetDestination(target.position);
	}

	public override void Reason(StateMachine machine) {
		if (Vector3.Distance(target.position, transform.position) > 5) { // TODO: Unhardcode this.
			machine.CurrentState = machine.GetState("IdleState");
		}
	}
}
