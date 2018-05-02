using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureSwitcher : MonoBehaviour {

	[SerializeField] private Material _goodState;
	[SerializeField] private Material _hostileState;
	private BehaviourState _behaviourState;
	
	[SerializeField]
	private Renderer _renderer;
	
	// Use this for initialization
	void Start () {
		_behaviourState = GetComponent<BehaviourState>();
		if(!_renderer) _renderer = GetComponent<Renderer>();
		_behaviourState.onStateChange += OnStateChange;
	}

	private void OnStateChange(BehaviourState.BehaviourStates behaviourStates) {
		switch (behaviourStates) {
			case BehaviourState.BehaviourStates.Friendly:
				_renderer.material = _goodState;
				break;
			case BehaviourState.BehaviourStates.Hostile:
				_renderer.material = _hostileState;
				break;
		}
	}
}
