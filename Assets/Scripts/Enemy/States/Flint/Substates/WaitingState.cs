
using UnityEngine;

/// <summary author="Antonio Bottelier">
/// This state requests it's master to go into a new state.
/// </summary>
public class WaitingState : State
{
    void Awake() => stateName = GetType().Name;

    private AttackingState state;
    
    public override void EnterState(StateMachine machine)
    {
        if(!state) state = transform.parent.GetComponent<AttackingState>();
        Debug.Log($"Entered state ${stateName}");

        state.RequestNewState();
        Debug.Log("Requesting new state.");
    }

    public override void Act(StateMachine machine) { }
    public override void Reason(StateMachine machine) { }
}