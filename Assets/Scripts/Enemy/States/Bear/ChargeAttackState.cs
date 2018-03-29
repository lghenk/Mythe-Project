using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Created by Timo Heijne
public class ChargeAttackState : State {
	[HideInInspector]
	public Transform target;
	
	private NavMeshAgent _navMeshAgent;
	private AnimationHandler _animationHandler;
 
	private void Start() {
		stateName = "ChargeAttackState";
		_navMeshAgent = GetComponent<NavMeshAgent>();
		_animationHandler = GetComponent<AnimationHandler>();
	}

	public override void EnterState(StateMachine machine) {
		_navMeshAgent.speed = 5;
		_navMeshAgent.isStopped = false;
		_animationHandler.SetAnimation("Charge Attack", true);
	}

	public override void Act(StateMachine machine) {
		_navMeshAgent.SetDestination(target.position);
	}

	public override void Reason(StateMachine machine) {
		if (Vector3.Distance(target.position, transform.position) > 25) { // TODO: Unhardcode this.
			machine.CurrentState = machine.GetState("IdleState");
		} else if (Vector3.Distance(target.position, transform.position) < 2) {
			machine.CurrentState = machine.GetState("DecideAttackState");
		}
	}

	public override void ExitState(StateMachine machine) {
		_navMeshAgent.isStopped = true;
		_animationHandler.SetAnimation("Charge Attack", false);
	}
}
