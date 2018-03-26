using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Created By: Timo Heijne
public abstract class State : MonoBehaviour {
    [HideInInspector]
    public string stateName;
    
    public virtual void EnterState(StateMachine machine) { }
    public abstract void Act(StateMachine machine);
    public abstract void Reason(StateMachine machine);
    public virtual void ExitState(StateMachine machine) { }
}
