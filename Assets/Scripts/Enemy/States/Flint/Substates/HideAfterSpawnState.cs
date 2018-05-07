using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class HideAfterSpawnState : State 
{
	void Awake() => stateName = GetType().Name;

	[SerializeField] private Transform hidingRock;
	[SerializeField] private float rockMoveSpeed = 7.0f;

	private BehaviourState[] _spawnedEnemies;
	private AnimationHandler _animationHandler;

	public override void EnterState(StateMachine machine)
	{
		if (!hidingRock) throw new Exception("You're missing a rock prefab.");
		if (!_animationHandler) _animationHandler = transform.parent.GetComponent<AnimationHandler>();

		Debug.Log($"Entered state ${stateName}");

		Vector3 rockheight = transform.position;
		rockheight.y = 0.0f;
		StartCoroutine(MoveRock(rockheight));
		_animationHandler.SetAnimation("t_Stomp");
	}

	private IEnumerator MoveRock(Vector3 to)
	{
		while ((to - hidingRock.position).magnitude > 0.01f)
		{
			hidingRock.position = Vector3.Lerp(hidingRock.position, to, rockMoveSpeed * Time.deltaTime);
			yield return new WaitForEndOfFrame();
		}

		hidingRock.position = to;
	}

	public override void Act(StateMachine machine) { }

	public override void Reason(StateMachine machine)
	{
		// just wait ... waiit.... 
		
		// count enemies that are either peaceful, or fuckinnn dead.
		int enemycount = 0;

		foreach (var spawnedEnemy in _spawnedEnemies)
		{
			if (spawnedEnemy == null || 
			    spawnedEnemy.State == BehaviourState.BehaviourStates.Friendly) continue;

			enemycount++;
		}

		if (enemycount != 0) return;
		
		foreach (var s in _spawnedEnemies)
		{
			if (s != null) Destroy(s);
		}
		
		machine.SwitchState("SubDizzyState");
	}

	public void SetSpawnedEnemies(BehaviourState[] e)
	{
		_spawnedEnemies = e;
	}

	public override void ExitState(StateMachine machine)
	{
		Vector3 rockheight = transform.position;
		rockheight.y = -4.0f;
		StartCoroutine(MoveRock(rockheight));
	}
}
