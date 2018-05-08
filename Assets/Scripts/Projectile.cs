using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	private Vector3 constantVelocity;
	private Health _playerHealth;
	private float damage;

	private const float MIN_LENGTH_BEFORE_HURT = 7.0f; // squared
	
	public void Init(GameObject player, Vector3 velocity, float damageOnHit)
	{
		_playerHealth = player.GetComponent<Health>();
		damage = damageOnHit;
		constantVelocity = velocity;
	}
	
	void Update ()
	{
		transform.position += constantVelocity * Time.deltaTime;

		if (_playerHealth == null) return;

		if ((_playerHealth.transform.position - transform.position).sqrMagnitude <= MIN_LENGTH_BEFORE_HURT)
		{
			_playerHealth?.TakeDamage(damage);
			// TODO : a nice particle effect ?
			// TODO : oh, and a nice sound! Like BRGGGGHGUGHUGHGH HGH G!!
			Destroy(gameObject);
		}
	}
}
