using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class TPCollision : MonoBehaviour
{
	private const float STEP_HEIGHT = 0.2f;
	private const float MAX_ANGLE = 89f;
	private const float RAYCAST_HEIGHT = 0.05f;

	[SerializeField] private LayerMask _collisionMask;
	private CapsuleCollider _collider;

	private void Start()
	{
		_collider = GetComponent<CapsuleCollider>();
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

		Vector3 origin = transform.position;
		Vector3 direction = moveVector;
		
		RaycastHit info;
		if (!Physics.Raycast(origin + Vector3.up * RAYCAST_HEIGHT, direction, out info, length, _collisionMask)) return false;
			
		angle = Vector3.Angle(info.normal, Vector3.up);
		if (angle > MAX_ANGLE || Mathf.Abs(angle) < float.Epsilon)
		{
			//velocity = velocity.normalized * info.distance;
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
		throw new NotImplementedException("nigga");
	}
	
	/// <summary>
	/// Checks with a capsule sweep if the player can move to the next position,
	/// if not, it adjusts the velocity accordingly to the maximum possible position
	/// </summary>
	/// <param name="velocity"></param>
	private void TestCollision(ref Vector3 velocity)
	{
		
	}

	// Move the character with the velocity
	public void Move(Vector3 velocity)
	{
		
		Vector3 vel = velocity;
		Vector3 moveVector = vel;
		moveVector.y = 0;
		bool walkingOnSlope = HandleSlope(vel, moveVector.magnitude, ref vel);
		//velocity = vel;
		transform.position += vel + Vector3.up * velocity.y;
        
		if (walkingOnSlope)
		{
			PushToGround();
		}
	}
	
	private void PushToGround()
	{
		RaycastHit hit;
		if (!Physics.Raycast(transform.position + Vector3.up * 0.25f, Vector3.down, out hit)) return;
		transform.position = hit.point;
	}
	
	public bool CheckGrounded(float gravity)
	{
		Vector3 p1 = transform.position + Vector3.up * (_collider.radius / 2);
		return Physics.Raycast(transform.position + Vector3.up * 0.5f, Vector3.down, -gravity + 0.5f, _collisionMask);
		//return Physics.CapsuleCast(p1, p1 + Vector3.up * (_collider.height) - Vector3.up * (_collider.radius / 2), _collider.radius,
		//	Vector3.up * gravity);
	}
}
