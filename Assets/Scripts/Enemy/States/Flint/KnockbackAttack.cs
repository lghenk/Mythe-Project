using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  ANTONIO
/// 
/// The knockback state will only play when the player gets too close
/// during it's attacking phase.
/// </summary>
public class KnockbackAttack : State
{
    void Awake() => stateName = GetType().Name;

    [Tooltip("How much to multiply the knockback velocity by.")]
    [SerializeField] private float velocityMultiplier = 1.0f;
    [Tooltip("How much the velocity decays exponentially.")]
    [SerializeField] private float velocityDecayMultiplier = 0.8f;
    
    private Vector3 knockbackVelocity;
    private Transform player;
    private TPCollision playerCollision;
    private AnimationHandler _animationHandler;
    private BlendshapeHandler _blendshapeHandler;

    
    void Start()
    {
        if (!player) player = GameObject.FindGameObjectWithTag("Player").transform;
        if (!playerCollision) playerCollision = player?.GetComponent<TPCollision>();
        if (!_animationHandler) _animationHandler = GetComponent<AnimationHandler>();
        if (!_blendshapeHandler) _blendshapeHandler = GetComponent<BlendshapeHandler>();
    }
    
    public override void EnterState(StateMachine machine)
    {
        knockbackVelocity = (player.position - transform.position).normalized * velocityMultiplier;
        _animationHandler.SetAnimation("t_Stomp");
        _blendshapeHandler.SetBlendshape("Flint_face.eyes_angry", 90f);
        _blendshapeHandler.SetBlendshape("Flint_face.angry", 90f);
        _blendshapeHandler.SetBlendshape("Flint_face.closed_mouth", 70f);
    }

    public override void Act(StateMachine machine) { }

    public override void Reason(StateMachine machine)
    {
        playerCollision.Move(knockbackVelocity);

        knockbackVelocity = Vector3.Lerp(knockbackVelocity, Vector3.zero, velocityDecayMultiplier * Time.deltaTime);

        if (knockbackVelocity.magnitude < 0.01f) machine.CurrentState = machine.GetState("AttackingState");
    }
}
