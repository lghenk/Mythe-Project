using System.Collections;
using System.Collections.Generic;
using QTInput;
using UnityEngine;
using ExtensionMethods;

public class PlayerInput : MonoBehaviour {

	private KeyboardInput _keyboardInput = new KeyboardInput();
	
	[SerializeField] private OrbitCamera _camera;
	[SerializeField] private ThirdPersonMovement _movement;
	[SerializeField] private PlayerJump _jump;

	[Header("settings")] [SerializeField] private float Sensitivity = 800f;

	private bool moving, looking, running, jump;
	
	void Start()
	{
		if(!_camera) _camera = Camera.main.GetComponent<OrbitCamera>();
		if(!_movement) _movement = GetComponent<ThirdPersonMovement>();
		if (!jump) _jump = GetComponent<PlayerJump>();
	}
	
	// TODO: Keyboard input should not override controller input.
	
	void Update ()
	{
		Reset();
		UpdateControllerInput();
		UpdateKeyboardInput();
	}

	void UpdateControllerInput()
	{
		
	}
	
	void UpdateKeyboardInput()
	{
		if (!moving)
		{
			_movement.SetMoveVector(_keyboardInput.MoveInput);
			moving = true;
		}

		if (!looking)
		{
			_camera.Rotate(_keyboardInput.MouseDelta * Sensitivity);
			looking = true;
		}

		if (!running)
		{
			_movement.Running = _keyboardInput.kRun;
		}

		if (!jump)
		{
			if(_keyboardInput.kJump.WasPressed) _jump.Jump();
		}
	}

	void Reset()
	{
		moving = looking = running = jump = false;
	}
}
