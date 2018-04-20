using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackInput : MonoBehaviour {

	private AnimationHandler _animHandler;

	private void Start() {
		_animHandler = GetComponent<AnimationHandler>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			_animHandler.SetAnimation("Attack");
		}
	}
}
