using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TPCollision))]
public class SimpleTPController : MonoBehaviour
{
    private TPCollision _tpCollision;
    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        _tpCollision = GetComponent<TPCollision>();
    }

    private void Update()
    {
        float speed = 5;

        velocity.x = velocity.z = 0;
        velocity.y += Time.deltaTime * Physics.gravity.y;
        
        bool grounded = _tpCollision.CheckGrounded(velocity.y);
        
        if (_tpCollision.CheckGrounded(velocity.y * Time.deltaTime)) velocity.y = 0;
        if (velocity.y < -100) velocity.y = -100;

        if (Input.GetKey(KeyCode.A)) velocity += Vector3.left * speed;
        if(Input.GetKey(KeyCode.D)) velocity += Vector3.right * speed;
        if(Input.GetKey(KeyCode.W)) velocity += Vector3.forward * speed;
        if (Input.GetKey(KeyCode.S)) velocity += Vector3.back * speed;

        Move(velocity * Time.deltaTime);
    }
    
    private void Move(Vector3 velocity)
    {
        Vector3 vel = velocity;
        Vector3 moveVector = vel;
        moveVector.y = 0;
         _tpCollision.GetSlope(vel, moveVector.magnitude, ref vel);
        velocity = vel;
        transform.position += vel;
    }
}
