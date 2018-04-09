using System;
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
        
        //if (_tpCollision.CheckGrounded(velocity.y * Time.deltaTime)) velocity.y = 0;
        if (velocity.y < -100) velocity.y = -100;

        Vector3 moveVector = Vector3.zero;
        if (Input.GetKey(KeyCode.A)) moveVector += Vector3.left;
        if(Input.GetKey(KeyCode.D)) moveVector += Vector3.right;
        if(Input.GetKey(KeyCode.W)) moveVector += Vector3.forward;
        if (Input.GetKey(KeyCode.S)) moveVector += Vector3.back;

        Vector3 vel = moveVector.normalized * speed + velocity;
        Move(vel * Time.deltaTime);
    }
    
    private void Move(Vector3 velocity)
    {
        
    }
}
