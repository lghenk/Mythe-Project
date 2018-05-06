using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LookAtOnState : MonoBehaviour
{

    [SerializeField] private StateMachine _stateMachine;
    [SerializeField] private Transform _target;
    [SerializeField] private bool _x, _y, _z, _lerp;
    [SerializeField] private float _lerpSpeed;
    [SerializeField] private string[] _statesToRotate;
    
    private void Start()
    {
        if (!_stateMachine)
            _stateMachine = GetComponent<StateMachine>();

        if (!_stateMachine)
        {
            Debug.LogWarning("LookAtOnState :: State machine not found, will rotate constantly.");
        }

        if (_statesToRotate == null || _statesToRotate.Length == 0)
        {
            Debug.LogWarning("LookAtOnState :: _statesToRotate is either null or empty, will rotate constantly.");
        }
    }
    
    private void LateUpdate()
    {
        if (_stateMachine && _statesToRotate != null && _statesToRotate.Length != 0)
        {
            if (!_statesToRotate.Any(x =>
                string.Equals(x, _stateMachine.CurrentState.stateName, StringComparison.CurrentCultureIgnoreCase)))
            {
                Debug.Log($"Current state is {_stateMachine.CurrentState.stateName}, returning.");
                return;
            }
        }
        
        Vector3 rotation = Quaternion.LookRotation(_target.position - transform.position, Vector3.up).eulerAngles;

        if (!_x) rotation.x = transform.eulerAngles.x;
        if (!_y) rotation.y = transform.eulerAngles.y;
        if (!_z) rotation.z = transform.eulerAngles.z;
        
        transform.rotation = _lerp ? 
            Quaternion.Lerp(transform.rotation, Quaternion.Euler(rotation), _lerpSpeed * Time.deltaTime)
            : Quaternion.Euler(rotation);
    }
}
