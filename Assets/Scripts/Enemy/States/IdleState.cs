using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Created By: Timo Heijne
/// This is acting as a "Generic" idle state we can place on any enemy we have
public class IdleState : State {

    [SerializeField]
    private bool _shouldWander = false;

    [SerializeField] 
    private State _wanderState = null;
    private AnimationHandler _animationHandler;

    [SerializeField, Tooltip("How long it should take for this AI to move on to next state autonomously (e.g. wander state for example)"), Range(0, 100)]
    private float _reasonTimerLength = 5;
    private float _reasonTimer;
    
    private void Start() {
        _animationHandler = GetComponent<AnimationHandler>();
    }

    public override void EnterState(StateMachine machine) {
        // Set idle animation state (if actor has idle anim state)
        _animationHandler?.SetAnimation("Idle", true);
        _reasonTimer = Time.timeSinceLevelLoad + _reasonTimerLength;
    }

    public override void Act(StateMachine machine) { }

    public override void Reason(StateMachine machine) {
        // TODO: Add more options for autonomous behaviour and randomize it then.
        if (_shouldWander && Time.timeSinceLevelLoad >= _reasonTimer) 
            machine.CurrentState = _wanderState;
    }
}