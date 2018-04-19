using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourState : MonoBehaviour {

	public enum BehaviourStates {
		Hostile,
		Friendly,
		DBNO
	}

	public Action<BehaviourStates> onStateChange;
	private BehaviourStates _behaviourState = BehaviourStates.Hostile;
	public BehaviourStates State => _behaviourState;

	public void SetState(BehaviourStates state) {
		_behaviourState = state;
		onStateChange?.Invoke(state);
	}
}
