using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TPCollision))]
public class ThirdPersonMovement : MonoBehaviour
{
    private Vector3 ControllerAxis => new Vector3(Input.GetAxis("Horizontal"), 
        Input.GetAxis("Vertical"));

    private const float MAX_GRAVITY = -100;
    
    [SerializeField] private float _movementSpeed = 2.0f;
    [SerializeField] private float _maxMoveSpeed;
    [SerializeField] private float _maxRotateSpeed = 3.0f;
    
    private bool moving = false;
    private float currentVelocity;
    private float currentGravity;
    
    private OrbitCamera _ob;
    private TPCollision _tpc;
    private Transform _cameraTransform;
    
    private void Start()
    {
        _ob = Camera.main.GetComponent<OrbitCamera>();
        _tpc = GetComponent<TPCollision>();
        _cameraTransform = Camera.main.transform;
    }
    
    private void Update()
    {
        ApplyGravity();
        Vector3 moveVector = GetMoveVector();
        Movement(moveVector);
        
        Vector3 velocity = moveVector * currentVelocity * Time.deltaTime + Vector3.up * currentGravity;
        _tpc.Move(velocity * Time.deltaTime);
    }

    private void ApplyGravity()
    {
        currentGravity += Physics.gravity.y * Time.deltaTime;
        if (currentGravity < MAX_GRAVITY)
            currentGravity = MAX_GRAVITY;

        if (_tpc.CheckGrounded(currentGravity * Time.deltaTime)) 
        {
            // move to ground
            currentGravity = 0;
        }
    }

    private Vector3 GetMoveVector()
    {
        Vector3 ca = ControllerAxis;
        Vector3 right = _cameraTransform.right;
        Vector3 forward = _cameraTransform.forward;

        right.y = forward.y = 0; 
        right.Normalize();
        forward.Normalize();

        return (ca.x * right + ca.y * forward).normalized;
    }

    private void Movement(Vector3 moveVector)
    {
        if (ControllerAxis.magnitude < 0.1) return;
        IncreaseVelocity();

        Vector2 tf = new Vector2(transform.forward.x, transform.forward.z);
        Vector3 mv = new Vector3(moveVector.x, moveVector.z);
        float dot = -(Vector2.Dot(tf, mv) - 1);
        float reduction = 0;
        
        if (dot > 0.1f)
        {
            reduction = currentVelocity * (dot / 2);
        }

        currentVelocity += -reduction;
        
        RotateToCamera(moveVector);
        _ob.RotateTowardsY(transform.eulerAngles.y, 60 * Time.deltaTime);
    }

    private void IncreaseVelocity()
    {
        currentVelocity += _movementSpeed * Time.deltaTime;
        if (currentVelocity > _maxMoveSpeed)
            currentVelocity = _maxMoveSpeed;
    }

    private void SlowDown()
    {
        
    }

    private void RotateToCamera(Vector3 moveVector)
    {
        Quaternion lookVector = Quaternion.LookRotation(moveVector);
        transform.rotation = 
            Quaternion.RotateTowards(transform.rotation, lookVector, _maxRotateSpeed * Time.deltaTime);
    }
}
