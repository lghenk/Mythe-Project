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

	protected StateHandler _stateHandler;

	protected StateHandler stateHandler => _stateHandler ?? (_stateHandler = gameObject.AddComponent<StateHandler>());

	public State CurrentState {
		get { return _currentState;  }
		set { 
			_currentState?.ExitState(this); // Exit Current State
			_currentState = value; 
			_currentState?.EnterState(this); // Enter new state
		}
	}
	
	// Use this for initialization
	public void Start () 
	{
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

	/// <summary author="Antonio Bottelier">
	/// Easy method for switching states
	/// </summary>
	public void SwitchState(string state)
	{
		CurrentState = GetState(state);
	}

	public State[] GetAllStates()
	{
		return stateHandler.GetAllStates();
	}
}