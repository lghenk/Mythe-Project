using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary author="Antonio Bottelier">
/// 
/// The boss will do a throw attack with a random gameobject, where it attaches a projectile script to it that damages the player.
/// It will also guess where the player will be 
/// 
/// </summary>
public class ThrowAttack : State
{
	private void Awake() => stateName = GetType().Name;

	[Tooltip("How many rocks Flint will throw before he's all dizzy and vulnerable.")]/////////
	[SerializeField] private int rocksToThrow = 4;
	[Tooltip("How long to wait between rock throw.")]//////////////////////////////////////////
	[SerializeField] private float timeBetweenRocks = 1.2f;
	[Tooltip("How fast will the projectiles go?")]/////////////////////////////////////////////
	[SerializeField] private float projectileSpeed = 20.0f;
	[SerializeField] private float projectileDamage = 10.0f;
	[Tooltip("What projectile to throw, if more than one, it will randomise it.")]
	[SerializeField] private GameObject[] throwingProjectileObjects;

	private GameObject _player;
	private Vector3 _lastPlayerPosition;
	private AnimationHandler _animationHandler;
	private BlendshapeHandler _blendshapeHandler;
	
	private int rocksLeft; // how many rocks are left to be thrown?
	private float lastRockThrown; // when was the last rock thrown?
	
	public override void EnterState(StateMachine machine)
	{
		if (!_player) _player = GameObject.FindGameObjectWithTag("Player");
		if (!_animationHandler) _animationHandler = transform.parent.GetComponent<AnimationHandler>();
		if (!_blendshapeHandler) _blendshapeHandler = transform.parent.GetComponent<BlendshapeHandler>();
		
		_blendshapeHandler.SetBlendshape("Flint_face.eyes_angry", 90f);
		_blendshapeHandler.SetBlendshape("Flint_face.angry", 90f);
		_blendshapeHandler.SetBlendshape("Flint_face.closed_mouth", 70f);
		
		rocksLeft = rocksToThrow;
		lastRockThrown = Time.time - timeBetweenRocks + 0.1f;
		_lastPlayerPosition = _player.transform.position;
	
	}

	public override void ExitState(StateMachine machine)
	{
		_blendshapeHandler.SetBlendshape("Flint_face.eyes_angry", 0f);
		_blendshapeHandler.SetBlendshape("Flint_face.angry", 0f);
		_blendshapeHandler.SetBlendshape("Flint_face.closed_mouth", 0f);
	}

	/// <summary>
	/// Calculate where the player will be, and throw at a different position based on the distance.
	/// </summary>
	private Vector3 CalculateTrajectory()
	{
		Vector3 playerVelocity = _player.transform.position - _lastPlayerPosition;
		
		
		float distance = (_player.transform.position - transform.position).magnitude;
		float seconds = distance / projectileSpeed; // how many seconds it will take before the projectile reaches the player

		Debug.Log((playerVelocity * seconds).ToString("f4"));
		Debug.Log(seconds);

		
		Vector3 nextPoint = _player.transform.position + playerVelocity * seconds / Time.deltaTime;
		nextpos = nextPoint;
		Debug.DrawLine(nextPoint, nextPoint + Vector3.up, Color.blue);
		Vector3 trajectory = (nextPoint - transform.position).normalized * projectileSpeed;
		
		return trajectory;
	}
	
	private void ThrowRock(Vector3 velocity)
	{
		if(throwingProjectileObjects == null || throwingProjectileObjects.Length == 0)
			throw new ArgumentException("No projectiles to throw, please include at least one Game Object in the array.");
		
		// select a random projectile
		GameObject projectile =
			throwingProjectileObjects[Random.Range(0, throwingProjectileObjects.Length)];

		var pg = Instantiate(projectile, transform.position,
			new Quaternion(Random.Range(0, 1), Random.Range(0, 1), Random.Range(0, 1), Random.Range(0, 1)));
		pg.AddComponent<Projectile>().Init(_player, velocity, projectileDamage);
		
		// play the throw animation.
		_animationHandler.SetAnimation("t_Throw");
	}

	public override void Act(StateMachine machine)
	{
		if (Time.time > lastRockThrown + timeBetweenRocks)
		{
			Vector3 trajectory = CalculateTrajectory();
			trajectory.y = 0;
			ThrowRock(trajectory);

			lastRockThrown = Time.time;
			rocksLeft--;
		}

		_lastPlayerPosition = _player.transform.position;
		
		if (rocksLeft > 0) return;
		
		Debug.Log("oof.. I am very dizzy... by god I am dizzy...");
		machine.SwitchState("SubDizzyState");
	}

	private Vector3 nextpos; 
	private void OnDrawGizmos()
	{
		Gizmos.color = new Color(1,0,0,0.2f);
		Gizmos.DrawSphere(nextpos, 0.5f);
	}

	public override void Reason(StateMachine machine)
	{
		
	}
}
