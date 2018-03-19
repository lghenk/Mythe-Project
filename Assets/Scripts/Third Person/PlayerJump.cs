using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private float _jumpHeight = 6;
    private TPCollision _col;
    private ThirdPersonMovement _tpm;

    private void Start()
    {
        _col = GetComponent<TPCollision>();
        _tpm = GetComponent<ThirdPersonMovement>();
    }

    public void Jump()
    {
        bool grounded = _col.CheckGrounded(-1, false);

        if (grounded)
        {
            _tpm.Gravity = _jumpHeight;
        }
    }
}
