using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Created by: Timo Heijne
/// <summary>
/// Keeps track of the current active state, Also handles the Enter, Exit Act & Reason on the states.
/// </summary>
public class StateMachine : MonoBehaviour {

	[SerializeField]
	private State _defaultState;
	private State _currentState;
	
	public State CurrentState {
		get { return _currentState;  }
		set { 
			_currentState?.ExitState(this); // Exit Current State
			_currentState = value; 
			_currentState?.EnterState(this); // Enter new state
		}
	}
	
	// Use this for initialization
	void Start () {
		State[] states = GetComponents<State>();
		Debug.Log("Found " + states.Length + " State(s)");
		CurrentState = _defaultState;
	}
	
	// Update is called once per frame
	void Update () {
		_currentState?.Act(this);
		_currentState?.Reason(this);
	}
}