
using UnityEngine;

public class SubDizzyState : State
{
    public StateMachine _masterMachine;

    private void Awake() => stateName = GetType().Name;

    public override void EnterState(StateMachine machine)
    {
        Debug.Log($"Entered state ${stateName}");
        
        if (!_masterMachine) _masterMachine = transform.parent.GetComponent<StateMachine>();
        
        _masterMachine.SwitchState("DizzyState");
    }

    public override void Act(StateMachine machine) {}

    public override void Reason(StateMachine machine)
    {
    }
}
