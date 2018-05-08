using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary author="Antonio Bottelier">
/// 
/// This little script handles the substates that the AI can have.
/// 
/// </summary>
public class AttackingState : State
{
	void Awake() => stateName = GetType().Name;

	[Tooltip("If the player is within these units (squared), the enemy will fire off it's knockback attack.")]
	[SerializeField] private float minPlayerSqrPlayerDistance = 5.0f;
	[Tooltip("The states that are impossible to be randomly selected.")]
	[SerializeField] private string[] impossibleRandomStates; // States that cannot be chosen.
	[Tooltip("The states that are impossible to be interrupted by the knockback attack.")]
	[SerializeField] private string[] uninterruptableStates; // States that cannot be interrupted, by for example a knockback attack.
	
	private StateMachine @this;
	private StateMachine subStateMachine;
	private State[] subStateMachineStates;
	private string lastSubState;

	private Transform player;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	public override void EnterState(StateMachine machine)
	{
		// if the player is too close, do a knockback attack instead.
		if (CheckAndDoKnockback(machine)) return;
		
		if (!@this) @this = machine;
		
		if (subStateMachine) subStateMachine.enabled = true;
		else
		{
			for (int i = 0; i < transform.childCount; i++)
			{
				var child = transform.GetChild(i);
				if ((subStateMachine = child.GetComponent<StateMachine>()) != null) break;
				subStateMachine?.Start(); // make an early call to start, because otherwise the random state does not work.
			}
		}

		Debug.Log("yo");
		
		SetRandomState();
	}

	private void SetRandomState()
	{
		if (!subStateMachine) Debug.Log("frick");
		
		if(subStateMachineStates == null) subStateMachineStates = subStateMachine.GetAllStates();
		
		// don't continuously have the same state. ( at least, if we have multiple states )

		State randomState;
		
		do
		{
			randomState =
				subStateMachineStates[Random.Range(0, subStateMachineStates.Length)];
		} while (impossibleRandomStates.Any(x => x == randomState.stateName) || randomState.stateName == lastSubState);

		subStateMachine.CurrentState = randomState;
		lastSubState = randomState.stateName;
	}

	public override void Act(StateMachine machine) {} // look, this one isn't going to be used!

	public override void Reason(StateMachine machine)
	{
		CheckAndDoKnockback(machine);
	}

	private bool CheckAndDoKnockback(StateMachine machine)
	{
		float distance = (player.position - transform.position).sqrMagnitude;

		if (distance < minPlayerSqrPlayerDistance)
		{
			if(uninterruptableStates.Any(x => x == subStateMachine.CurrentState.stateName)) return false;
			machine.SwitchState("KnockbackAttack");
			return true;
		}

		return false;
	}

	public override void ExitState(StateMachine machine)
	{
		subStateMachine.enabled = false;
	}

	public void RequestNewState()
	{
		SetRandomState();
	}
}
