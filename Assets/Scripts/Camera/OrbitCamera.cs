using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    public bool Follow { get; set; }
    public float Sensitivity { get; set; }
    public Transform Target { get; set; }
    public Vector2 MouseDelta => new Vector2(Input.GetAxis("Mouse X") * Sensitivity, 
        Input.GetAxis("Mouse Y") * Sensitivity);

    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _sens = 1.0f;
    [SerializeField] private bool _smoothRotation, _smoothPosition;
    [SerializeField] private float _smoothRotAmount = 1.0f, _smoothPosAmount = 1.0f;
    [SerializeField] private LayerMask _cameraLayerMask;

    private Quaternion _rotation;
    
    private void Awake()
    {
        Follow = true;
        Sensitivity = _sens;
        Target = _target;

        _rotation = transform.rotation;
    }
    
    private void LateUpdate()
    {
        if (Target == null || !Follow) return;

        var mouseDelta = MouseDelta;
        
        var wantedRotation = _rotation.eulerAngles;
        wantedRotation.y += mouseDelta.x * Time.deltaTime;
        wantedRotation.x += mouseDelta.y * Time.deltaTime;
        wantedRotation.z = 0;
        
        Quaternion WR = Quaternion.Euler(wantedRotation);
        
        transform.rotation = _smoothRotation
            ? Quaternion.Lerp(transform.rotation, WR, _smoothRotAmount * Time.deltaTime)
            : WR;

        _rotation = WR;

        var wantedPosition = Target.position + 
                             (transform.forward * -_offset.z) + (transform.right * _offset.x) +
                             (transform.up * _offset.y);

        AvoidCollision(ref wantedPosition);

        transform.position = _smoothPosition ? Vector3.Lerp(transform.position, 
            wantedPosition, Time.deltaTime * _smoothPosAmount) : wantedPosition;
    }

    public void RotateTowardsY(float yRotation, float rotationSpeed)
    {
        Vector3 tempEuler = transform.eulerAngles;
        tempEuler.y = yRotation;
        
        Quaternion wantedRotation = Quaternion.Euler(tempEuler);
        wantedRotation = Quaternion.RotateTowards(transform.rotation, wantedRotation, rotationSpeed);

        _rotation = Quaternion.Lerp(_rotation, wantedRotation, Time.deltaTime * 5);
    }


    private void AvoidCollision(ref Vector3 position)
    {
        Vector3 targetpos = Target.position + Vector3.up * _offset.y;
        Vector3 direction =  position - targetpos;

        RaycastHit info;
        if (!Physics.SphereCast(targetpos, 0.2f, direction.normalized, out info, direction.magnitude,
            _cameraLayerMask)) return;

        position = info.point + info.normal * 0.15f;
    }
}
