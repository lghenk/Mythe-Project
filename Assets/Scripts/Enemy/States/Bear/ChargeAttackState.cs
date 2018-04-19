using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Configuration;
using UnityEngine;
using UnityEngine.AI;

// Created by Timo Heijne
public class ChargeAttackState : State {
	[HideInInspector]
	public Transform target;

	[SerializeField]
	private float _chargeRange = 25;
	[SerializeField]
	private float _attackRange = 3;
	
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
		_animationHandler.SetAnimation("Charging", true);
	}

	public override void Act(StateMachine machine) {
		_navMeshAgent.SetDestination(target.position);
	}

	public override void Reason(StateMachine machine) {		
		if (Vector3.Distance(target.position, transform.position) > _chargeRange) {
			machine.CurrentState = machine.GetState("IdleState");
		} else if (Vector3.Distance(target.position, transform.position) < _attackRange) {
			_navMeshAgent.isStopped = true;
			_animationHandler.SetAnimation("Attack");
			
			machine.CurrentState = machine.GetState("IdleState");
		}
	}

	public override void ExitState(StateMachine machine) {
		_navMeshAgent.isStopped = true;
		_animationHandler.SetAnimation("Charging", false);
	}
}
