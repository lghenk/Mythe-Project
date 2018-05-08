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

	public Action OnAttack;

	private void Start() {
		stateName = "StaticAttackState";
		
		_animationHandler = GetComponent<AnimationHandler>();
	}

	public override void EnterState(StateMachine machine) {
		_animationHandler.SetAnimation("Idle", true);
		_animationHandler.SetAnimation("Attack");
		_animationHandler.onAnimationFinish += OnAnimationFinish;
		OnAttack?.Invoke();
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

	private void OnAnimationFinish(string s) {
		_goBackToIdle = true;
		_animationHandler.onAnimationFinish -= OnAnimationFinish;
	}
}
