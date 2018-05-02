using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackInput : MonoBehaviour {

	private MeleeCombat _meleeCombat;
	private AnimationHandler _animationHandler;
	
	private void Start() {
		_meleeCombat = GetComponent<MeleeCombat>();
		_animationHandler = GetComponent<AnimationHandler>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			_meleeCombat.Attack();
			_animationHandler.SetAnimation("Attack");
		}
	}
}
