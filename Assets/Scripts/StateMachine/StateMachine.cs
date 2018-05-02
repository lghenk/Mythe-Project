using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary author="Timo Heijne">
/// Keeps track of the current active state, Also handles the Enter, Exit Act & Reason on the states.
/// </summary>
public class StateMachine : MonoBehaviour {

	[SerializeField]
	private State _defaultState;
	private State _currentState;

	protected StateHandler stateHandler;
	
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
		stateHandler = gameObject.AddComponent<StateHandler>();
		State[] states = GetComponents<State>();
		//Debug.Log($"{gameObject.name} :: Found {states.Length} State(s)");
		CurrentState = _defaultState;
	}
	
	// Update is called once per frame
	void Update () { // TODO: Change to cooroutine to make ajustable tick rate
		_currentState?.Act(this);
		_currentState?.Reason(this);
	}

	public State GetState(string state) {
		//Debug.Log($"{gameObject.name} :: Attempting to switch to state: {state}");
		return stateHandler.GetState(state);
	}
}