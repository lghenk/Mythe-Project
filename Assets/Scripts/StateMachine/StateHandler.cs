using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateHandler : MonoBehaviour {

	private Dictionary<string, State> _states = new Dictionary<string, State>();
	
	// Use this for initialization
	void Start () {
		State[] states = GetComponents<State>();
		
		foreach (var state in states) {
			_states.Add(state.stateName, state);
		}
	}

	public State GetState(string state) {
		return _states[state];
	}
	
}
