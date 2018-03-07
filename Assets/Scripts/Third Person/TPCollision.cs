using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class TPCollision : MonoBehaviour
{
	private const float STEP_HEIGHT = 0.2f;
	private const float MAX_ANGLE = 60f;
	private const float RAYCAST_HEIGHT = 0.1f;

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
	/// <returns>angle of the slope</returns>
	public void GetSlope(Vector3 moveVector, float distance, ref Vector3 velocity)
	{
		float angle = 0;
		float length = velocity.magnitude;
		float distanceLeft = length;

		Vector3 origin = transform.position;
		Vector3 direction = moveVector;
		
		// First, move the player in front of the edge.
		// then, shoot a ray to the direction that the player (or the slope) is in.
		// If the ray hits, adjust the velocity accordingly if the angle is lower than max angle.
		// If a ray does not hit, then just move the player in the direction. (After doing a sweeping collision test for the entire body of the player)
		do
		{
			RaycastHit info;
			
			if (!Physics.Raycast(origin + Vector3.up * RAYCAST_HEIGHT, direction, out info, distanceLeft, _collisionMask)) return;
			
			angle = Vector3.Angle(info.normal, Vector3.up);
			if (angle > MAX_ANGLE || Mathf.Abs(angle) < float.Epsilon)
			{
				velocity = velocity.normalized * info.distance;
				return;
			}

			float lengthToSlope = RAYCAST_HEIGHT * Mathf.Tan(angle * Mathf.Deg2Rad);
			if (distanceLeft - info.distance > lengthToSlope)
			{
				origin += direction * lengthToSlope;
				distanceLeft -= lengthToSlope;
				continue;
			}

			Vector3 crossLeft = Vector3.Cross(info.normal, Vector3.up);
			direction = Vector3.Cross(info.normal, Vector3.up);
			
			float dist = distance - info.distance;
			velocity.y = -(distance / Mathf.Tan(angle * Mathf.Deg2Rad));
			velocity = velocity.normalized * length;
			
		} while (distanceLeft > 0);
		
		
	}
	
	public bool CheckGrounded(float gravity)
	{
		Vector3 p1 = transform.position + Vector3.up * (_collider.radius / 2);
		return Physics.Raycast(transform.position + Vector3.up * RAYCAST_HEIGHT, Vector3.down, -gravity + RAYCAST_HEIGHT, _collisionMask);
		//return Physics.CapsuleCast(p1, p1 + Vector3.up * (_collider.height) - Vector3.up * (_collider.radius / 2), _collider.radius,
		//	Vector3.up * gravity);
	}
}
