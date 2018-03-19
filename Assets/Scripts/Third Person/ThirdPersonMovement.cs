using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TPCollision))]
public class ThirdPersonMovement : MonoBehaviour
{
    public bool Running { get; set; }
    public float Gravity
    {
        set { currentGravity = value; }
        get { return currentGravity; }
    }

    private const float MAX_GRAVITY = -100;
    
    [SerializeField] private float _movementSpeed = 2.0f;
    [SerializeField] private float _runSpeedModifier = 1.5f;
    [SerializeField] private float _maxMoveSpeed;
    [SerializeField] private float _maxRotateSpeed = 3.0f;
    
    private bool moving = false;
    private float currentVelocity;
    private float currentGravity;
    
    private OrbitCamera _ob;
    private TPCollision _tpc;
    private Transform _cameraTransform;
    private Vector3 _moveVector;
    private Vector3 _lastMoveVector;
    
    private void Start()
    {
        _ob = Camera.main.GetComponent<OrbitCamera>();
        _tpc = GetComponent<TPCollision>();
        _cameraTransform = Camera.main.transform;
    }
    
    private void Update()
    {
        Vector3 moveVector = _moveVector;
        ApplyGravity();
        if (!Movement(_moveVector))
        {
            SlowDown(_movementSpeed);
            moveVector = transform.forward;
            moveVector.y = 0;
            moveVector.Normalize();
        }

        float speedModifier = Running ? _runSpeedModifier : 1.0f;
        Vector3 velocity = moveVector * currentVelocity 
                           + Vector3.up * currentGravity;
        
        #if UNITY_EDITOR
        Vector3 origin = transform.position + Vector3.up * 0.1f;
        Debug.DrawLine(origin, origin + velocity, Color.red);
        #endif
        
        _tpc.Move(velocity * Time.deltaTime);
    }

    private void ApplyGravity()
    {
        currentGravity += Physics.gravity.y * Time.deltaTime;
        if (currentGravity < MAX_GRAVITY)
            currentGravity = MAX_GRAVITY;

        if (_tpc.CheckGrounded(currentGravity * Time.deltaTime, placeOnGround:true)) 
        {
            currentGravity = 0;
        }
    }

    public void SetMoveVector(Vector3 ControllerAxis)
    {
        Vector3 ca = ControllerAxis;
        Vector3 right = _cameraTransform.right;
        Vector3 forward = _cameraTransform.forward;

        right.y = forward.y = 0; 
        right.Normalize();
        forward.Normalize();

        _moveVector = (ca.x * right + ca.y * forward).normalized;
    }

    private bool Movement(Vector3 moveVector)
    {
        if (_moveVector.magnitude < 0.1)
        {
            return false;
        }
            
        IncreaseVelocity(_movementSpeed);
        

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
        _lastMoveVector = moveVector;

        return true;
    }

    private void IncreaseVelocity(float speed)
    {
        float modifier = Running ? _runSpeedModifier : 1.0f;
        AddVelocity(speed);
        if (currentVelocity > _maxMoveSpeed * modifier)
            currentVelocity = _maxMoveSpeed * modifier;
    }

    private void SlowDown(float speed)
    {
        AddVelocity(-speed);
        if (currentVelocity < 0)
            currentVelocity = 0;
    }

    private void AddVelocity(float speed)
    {
        currentVelocity += speed * Time.deltaTime;
    }

    private void RotateToCamera(Vector3 moveVector)
    {
        Quaternion lookVector = Quaternion.LookRotation(moveVector);
        transform.rotation = 
            Quaternion.RotateTowards(transform.rotation, lookVector, _maxRotateSpeed * Time.deltaTime);
    }
}
