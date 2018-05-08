using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementAnims : MonoBehaviour {

	private ThirdPersonMovement _tpm;
	private AnimationHandler _ah;
	
	[SerializeField]
	private ParticleSystem _ps;
	
	// Use this for initialization
	void Start () {
		_tpm = GetComponent<ThirdPersonMovement>();
		_ah = GetComponent<AnimationHandler>();
	}
	
	// Update is called once per frame
	void Update () {
		if (_tpm.CurrentVelocity == 0) {
			_ps?.Stop();
			_ah.SetAnimation("Idle", true);
			_ah.SetAnimation("Walking", false);
		} else {
			_ps?.Play();
			_ah.SetAnimation("Idle", false);
			_ah.SetAnimation("Walking", true);
		}
	}
}
