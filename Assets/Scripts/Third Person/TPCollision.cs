using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class TPCollision : MonoBehaviour
{
	private const float STEP_HEIGHT = 0.2f;
	private const float MAX_ANGLE = 50f;
	private const float RAYCAST_HEIGHT = 0.05f;

	[SerializeField] private LayerMask _collisionMask;
	private CapsuleCollider _collider;

	/// <summary>
	/// Move the character with the velocity, without gravity
	/// </summary>
	/// <param name="velocity">velocity to go to</param>
	public void Move(Vector3 velocity)
	{
		Vector3 vel = velocity;
		Vector3 moveVector = vel;
		moveVector.y = 0;
		bool walkingOnSlope = HandleSlope(vel, moveVector.magnitude, ref vel);
		TestCollision(ref vel);
		
		transform.position += vel;
        
		if (walkingOnSlope)
		{
			PushToGround();
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
		Vector3 normal = info.normal;
		normal.y = 0;
		normal.Normalize();

		Vector3 moveDir = Vector3.Cross(normal, Vector3.up);
		Vector3 newVelocity = Vector3.Project(velocity, moveDir);
		velocity = newVelocity;
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

		Vector3 direction = moveVector;
		Vector3 origin = transform.position;
		
		RaycastHit info;
		if (!Physics.Raycast(origin + Vector3.up * RAYCAST_HEIGHT, direction, out info, moveDistance + 
		                                                                                _collider.radius / 2, _collisionMask)) return false;
			
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
		//velocity = velocity.normalized * moveDistance;

		return true;
	}

	/// <summary>
	/// Handles the player moving down the slope,
	/// if he is, then adjust the velocity accordingly so it doesn't skip down.
	/// </summary>
	/// <returns>If the player is moving down.</returns>
	private bool HandleDownSlope(Vector3 moveVector, float distance, ref Vector3 velocity)
	{
		throw new NotImplementedException("nigga");
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

		return (Physics.CapsuleCast(p1, p2, _collider.radius - 0.05f, velocity.normalized, out info, velocity.magnitude,
			_collisionMask));
	}
	
	private void PushToGround()
	{
		RaycastHit hit;
		if (!Physics.Raycast(transform.position + Vector3.up * 0.25f, Vector3.down, out hit)) return;
		transform.position = hit.point;
	}
	
	public bool CheckGrounded(float gravity, bool placeOnGround = false)
	{
		Vector3 p1 = transform.position + Vector3.up * (_collider.radius + _collider.height);
		Vector3 p2 = transform.position + Vector3.up * _collider.radius;
		RaycastHit hit;
		bool grounded = Physics.Raycast(transform.position + Vector3.up * 0.25f, Vector3.down, out hit, -gravity + 0.25f,_collisionMask);
		//bool grounded = Physics.CapsuleCast(p1, p2, _collider.radius, Vector3.down, out hit, -gravity + 0.5f, _collisionMask);

		if (placeOnGround && grounded)
		{
			transform.position = hit.point;
		}
		
		return grounded;
	}
}
