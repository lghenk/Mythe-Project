using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private float _jumpHeight = 6;
    [SerializeField] private float _minJumpTime = 0.2f;
    private TPCollision _col;
    private ThirdPersonMovement _tpm;

    private void Start()
    {
        _col = GetComponent<TPCollision>();
        _tpm = GetComponent<ThirdPersonMovement>();
    }

    public void Jump()
    {
        if (Time.time > _minJumpTime + _col.LastGroundTime) return;
        
        _tpm.Gravity = _jumpHeight;
        print("jumped");
    }
}
