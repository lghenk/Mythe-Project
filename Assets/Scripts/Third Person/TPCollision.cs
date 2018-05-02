using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Remoting.Messaging;
using UnityEditor;
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
	private const float MAX_ANGLE = 40;
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
		
		UpdateGrounded(vel.y);
		
		if (Grounded)
		{
			vel.y = 0;
			_lastGroundTime = Time.time;
			transform.position = GroundedInfo.point;//  + Vector3.up * RAYCAST_HEIGHT; 
		}
		
		
		
		bool walkingOnSlope = HandleSlope(vel, moveVector.magnitude, ref vel);

		if (walkingOnSlope) Debug.Log("tru af boy");
		
		TestCollision(ref vel);
		
		transform.position += vel;
        
		if (vel.y >= 0)//walkingOnSlope)
		{
			PushToGround();
		}

		//PushToGround();
	}

	private void PartMove(Vector3 velocity)
	{
		
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

		float y = velocity.y;
		Vector3 moveDir = Vector3.Cross(normal, Vector3.up);
		Vector3 newVelocity = Vector3.Project(velocity, moveDir);
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
		float moveDistance = velocity.magnitude;

		Vector3 direction = moveVector;
		Vector3 origin = transform.position;
		
		RaycastHit info;
		//if (!Physics.Raycast(origin + Vector3.up * RAYCAST_HEIGHT, direction, out info, 
		//	moveDistance + _collider.radius / 2, _collisionMask, QueryTriggerInteraction.Ignore)) return false;

		if (!GetCapsuleCast(velocity, out info)) return false;		
				
		angle = Vector3.Angle(info.normal, Vector3.up);
		if (angle > MAX_ANGLE || Mathf.Abs(angle) < float.Epsilon)
		{
			return false;
		}

		float y = Mathf.Sin(angle * Mathf.Deg2Rad) * moveDistance;

		if (!(velocity.y <= y)) return false;

		velocity.y = y + 0.001f;
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
		if (GetCapsuleCast(velocity, out hit)) velocity = Vector3.zero;
	}

	private bool GetCapsuleCast(Vector3 velocity, out RaycastHit info )
	{
		Vector3 p1, p2;
		p1 = transform.position + Vector3.up * (_collider.height - _collider.radius);
		p2 = transform.position + Vector3.up * (_collider.radius);

		return (Physics.CapsuleCast(p1, p2, _collider.radius, velocity.normalized, out info, velocity.magnitude,
			_collisionMask, QueryTriggerInteraction.Ignore));
	}

	#if UNITY_EDITOR
	private void OnDrawGizmos()
	{
		if (!Application.isPlaying) return;
		
		Vector3 p1, p2;
		p1 = transform.position + Vector3.up * (_collider.height - _collider.radius);
		p2 = transform.position + Vector3.up * (_collider.radius);

		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(p1, _collider.radius+0.01f);
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(p2, _collider.radius);
	}
	#endif
	
	private void PushToGround()
	{
		RaycastHit hit;
		if (!Physics.Raycast(transform.position + Vector3.up * 0.5f, Vector3.down, out hit, 0.5f)) return;
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
			length, _collisionMask, QueryTriggerInteraction.Ignore);
	}
}
