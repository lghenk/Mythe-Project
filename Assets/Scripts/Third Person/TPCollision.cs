using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class TPCollision : MonoBehaviour
{
	public bool Grounded => _grounded;
	public float LastGroundTime => _lastGroundTime;
	public RaycastHit GroundedInfo => _groundedInfo;
	
	private bool _grounded = false;
	private float _lastGroundTime;
	private RaycastHit _groundedInfo;
	
	private const float STEP_HEIGHT = 0.2f;
	private const float MAX_ANGLE = 50;
	private const float RAYCAST_HEIGHT = 0.05f;

	[SerializeField] private LayerMask _collisionMask;
	private CapsuleCollider _collider;
	
	#if UNITY_EDITOR
	private bool hitslope = false;
	private Vector3 hitpoint;
	private Vector3 normal;
	
	#endif

	/// <summary>
	/// Move the character with the velocity, without gravity
	/// </summary>
	/// <param name="velocity">velocity to go to</param>
	public void Move(Vector3 velocity)
	{
		Vector3 vel = velocity;
		Vector3 moveVector = vel;
		moveVector.y = 0;
		
		UpdateGrounded(vel.y);
		
		if (Grounded)
		{
			vel.y = -0;
			_lastGroundTime = Time.time;
			transform.position = GroundedInfo.point;//  + Vector3.up * RAYCAST_HEIGHT; 
		}
		
		bool walkingOnSlope = HandleSlope(vel, moveVector.magnitude, ref vel);
		
		if (walkingOnSlope) vel = vel.normalized * moveVector.magnitude;
		
		TestCollision(ref vel);
		
		transform.position += vel;
		
		if (walkingOnSlope)
		{
			//PushToGround();
		}
	}
	
	private void Start()
	{
		_collider = GetComponent<CapsuleCollider>();
	}

	/// <summary>
	/// Adjusts the velocity so that the player doesn't immediately stop against the wall, but slides along it.
	/// </summary>
	/// <param name="velocity">The current velocity to adjust</param>
	/// <param name="info">The raycast info to use</param>
	private void SlideAlongWall(ref Vector3 velocity, RaycastHit info)
	{
		float angle = Vector3.Angle(info.normal, Vector3.up);
		if (angle > 5 || angle < MAX_ANGLE) return;
		
		Vector3 normal = info.normal;
		normal.y = 0;
		normal.Normalize();

		float y = velocity.y;
		Vector3 vel = velocity;
		vel.y = 0;
		Vector3 moveDir = Vector3.Cross(normal, Vector3.up);
		Vector3 newVelocity = Vector3.Project(vel, moveDir);
		velocity = newVelocity;
		velocity.y = y;
	}
	
	/// <summary>
	/// Gets the angle of the slope of the next position, and sets the reference velocity to that.
	/// or something
	/// </summary>
	/// <returns>if the controller is walking on the slope.</returns>
	private bool HandleSlope(Vector3 moveVector, float distance, ref Vector3 velocity)
	{
		float angle = 0;
		float length = velocity.magnitude;
		float moveDistance = moveVector.magnitude;

		Vector3 direction = Vector3.down;//moveVector;
		Vector3 origin = transform.position;
		
		RaycastHit info;
		if (!Physics.Raycast(origin + velocity + Vector3.up * 2, direction, out info, 
			2 /*moveDistance + _collider.radius / 2*/, _collisionMask)) return false;
			
		angle = Vector3.Angle(info.normal, Vector3.up);
		if (angle > MAX_ANGLE || Mathf.Abs(angle) < float.Epsilon)
		{
			return false;
		}

		float y = Mathf.Sin(angle * Mathf.Deg2Rad) * moveDistance;

		if (!(velocity.y <= y)) return false;

		velocity.y = y;
		velocity.x = Mathf.Cos(angle * Mathf.Deg2Rad) * velocity.x;
		velocity.z = Mathf.Cos(angle * Mathf.Deg2Rad) * velocity.z;
		velocity = velocity.normalized * moveDistance;

		return true;
	}

	/// <summary>
	/// Handles the player moving down the slope,
	/// if he is, then adjust the velocity accordingly so it doesn't skip down.
	/// </summary>
	/// <returns>If the player is moving down.</returns>
	private bool HandleDownSlope(Vector3 moveVector, float distance, ref Vector3 velocity)
	{
		throw new NotImplementedException("Not implemented yet. Sorry, man.");
	}
	
	/// <summary>
	/// Checks with a capsule sweep if the player can move to the next position,
	/// if not, it adjusts the velocity accordingly to the maximum possible position
	/// </summary>
	/// <param name="velocity"></param>
	private void TestCollision(ref Vector3 velocity)
	{
		RaycastHit hit;

		if (!GetCapsuleCast(velocity, out hit)) return;

		SlideAlongWall(ref velocity, hit);
	}

	private bool GetCapsuleCast(Vector3 velocity, out RaycastHit info )
	{
		Vector3 p1, p2;
		p1 = transform.position + Vector3.up * (_collider.height - _collider.radius);
		p2 = transform.position + Vector3.up * (_collider.radius);

		return (Physics.CapsuleCast(p1, p2, _collider.radius - 0.001f, velocity.normalized, out info, velocity.magnitude,
			_collisionMask));
	}
	
	private void PushToGround()
	{
		RaycastHit hit;
		if (!Physics.Raycast(transform.position + Vector3.up * RAYCAST_HEIGHT, Vector3.down, out hit)) return;
		transform.position = hit.point;	
	}

	private void UpdateGrounded(float velocity)
	{		
		if (velocity > 0.1f)
		{
			_grounded = false;
			return;
		}

		float length = Mathf.Abs(velocity) + RAYCAST_HEIGHT;
		
		const float castHeight = 0.25f;
		_grounded = Physics.Raycast(transform.position + Vector3.up * RAYCAST_HEIGHT, Vector3.down, out _groundedInfo, 
			length, _collisionMask);
	}
	
	#if UNITY_EDITOR
	private void OnDrawGizmos()
	{
		
	}
	#endif
}
