using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Created by Timo Heijne
public class StaticAttackState : State {
	[HideInInspector]
	public Transform target;

	[SerializeField]
	private float _staticRange = 5;
	
	private AnimationHandler _animationHandler;
	private bool _goBackToIdle = false;
	
	


	private void Start() {
		stateName = "StaticAttackState";
		
		_animationHandler = GetComponent<AnimationHandler>();
	}

	public override void EnterState(StateMachine machine) {
		_animationHandler.SetAnimation("Idle", true);
		_animationHandler.SetAnimation("Attack");
		_goBackToIdle = false;
	}

	public override void Act(StateMachine machine) { }

	public override void Reason(StateMachine machine) {
		if (Vector3.Distance(target.position, transform.position) > _staticRange || _goBackToIdle) {
			machine.CurrentState = machine.GetState("DecideAttackState");
		}
	}

	public override void ExitState(StateMachine machine) {
		_animationHandler.SetAnimation("Idle", false);
	}

	IEnumerable ReturnToIdle() {
		yield return new WaitForSeconds(1f);
		_goBackToIdle = true;
	}
}
