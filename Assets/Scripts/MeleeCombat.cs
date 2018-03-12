﻿using UnityEngine;

public class MeleeCombat : MonoBehaviour {
	[SerializeField]
	private MeleeType _meleeType;

	[SerializeField]
	private Vector3 _attackOffset;

	private bool _inAttack;

	private void Update() {
		if (Input.GetButtonDown("Fire 1")) {
			Attack();
		}
	}

	private void Attack() {
		RaycastHit[] hits = Physics.SphereCastAll(transform.position + _attackOffset, _meleeType.Radius, Vector3.forward, _meleeType.Radius);

		for (int i = 0; i < hits.Length; i++) {
			
		}
	}
	
	private void OnDrawGizmosSelected() {
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(transform.position + _attackOffset, _meleeType.Radius);
	}
}