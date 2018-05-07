using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DizzyState : State
{
    public float dizzyTime;
    public float startingHP = 5000;
    
    private void Awake() => stateName = GetType().Name;

    private Health _health; // the boss can only be attacked in this particular state, so we'll be controlling it from here.
    private AnimationHandler _animationHandler;
    private BlendshapeHandler _blendshapeHandler;

    private bool healthSet = false;
    
    private void Start()
    {
        _animationHandler = GetComponent<AnimationHandler>();
        _blendshapeHandler = GetComponent<BlendshapeHandler>();
        _health = gameObject.AddComponent<Health>();
    }
    
    public override void EnterState(StateMachine machine)
    {
        if (!healthSet)
        {
            _health.SetHealth(startingHP);
            _health.onDamage += (f, f1, arg3, arg4) => _animationHandler.SetAnimation("t_Hit");
            healthSet = true;
        }
        
        var c = StartCoroutine(DizzyWait(machine));
        StartCoroutine(CheckDeath(machine, c));
        
        _animationHandler.SetAnimation("t_Paralised");
        _animationHandler.SetAnimation("b_Paralised", true);
        
        _blendshapeHandler.SetBlendshape("Flint_face.super_sad", 75);
        _blendshapeHandler.SetBlendshape("Flint_face.open_mouth", 100);
        _blendshapeHandler.SetBlendshape("Flint_face.eyes_wide_open", 100);
    }

    public override void ExitState(StateMachine machine)
    {
        _animationHandler.SetAnimation("b_Paralised", false);
    }

    /// <summary>
    /// This function will just continuosly check whether the player died or not,
    /// if it did, it will interrupt the DizzyWait coroutine and go sraight into the  
    /// </summary>
    private IEnumerator CheckDeath(StateMachine machine, Coroutine dizzywait)
    {
        while (machine.CurrentState == this)
        {
            if (_health.IsDead()) 
            {
                // If the boss is dead, stop the dizzy wait coroutine and go straight into the dead boss state
                StopCoroutine(dizzywait);

                machine.SwitchState("BossDeadState");
            }
                
            yield return new WaitForEndOfFrame(); 
        }
    }

    private IEnumerator DizzyWait(StateMachine machine)
    {
        yield return new WaitForSeconds(dizzyTime); // literally all this does is wait...

        machine.CurrentState = machine.GetState("AttackingState");
    }

    public override void Act(StateMachine machine) { } // why do we have two functions that do the same thing?!

    public override void Reason(StateMachine machine)
    {
        
    }
}
