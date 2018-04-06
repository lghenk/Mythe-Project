using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public abstract class HealthBaseModule : MonoBehaviour {
	protected Health health;
	public abstract void OnDamage(float damageAmount, float curHeath, float startingHealth);
	public abstract void OnDeath();

	public void SetHealthReference(Health localHealth) {
		health = localHealth;
	}
}
