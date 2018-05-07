using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary author="Antonio Bottelier">
/// 
/// This state will spawn 3 enemies of a random type, and then go into a hiding state while
/// passing the 3 enemies to the hiding script.
/// 
/// </summary>
public class SpawnAttack : State {
	
	void Awake() => stateName = GetType().Name;

	[Tooltip("The ememies that are capable of spawning. You could even put a rock here. See if I care.")]
	[SerializeField] private BehaviourState[] enemiesToSpawn;
	[Tooltip("The enemy will wait a bit before spawning each enemies, so it's not instant. (Which looks better) But.. how long?")]
	[SerializeField] private float _timeBetweenSpawn = 0.4f;
	[Tooltip("How much the enemies that are spawned on the side will spawn apart from the boss.")]
	[SerializeField] private float _enemySideDistance = 0.6f;
	private HideAfterSpawnState _h;
	private Transform _player;
	
	private AnimationHandler _animationHandler;
	private BlendshapeHandler _blendshapeHandler;

	
	private void Init()
	{
		if (_h) return;
		
		_h = GetComponent<HideAfterSpawnState>();
		_player = GameObject.FindGameObjectWithTag("Player").transform;
		_animationHandler = transform.parent.GetComponent<AnimationHandler>();
		_blendshapeHandler = transform.parent.GetComponent<BlendshapeHandler>();

	}

	public override void EnterState(StateMachine machine)
	{
		Init();
		
		Debug.Log($"Entered state ${stateName}");
		
		// decide the 3 spawn positions
		// the positions are kind of static, but still need to be calculated.
		// one is always in between flint and the player.
		// and the one is to the right and left side of the enemy, but still rotated towards the player. Wow. amazing.

		Vector3[] spawnPositions = new Vector3[3];

		spawnPositions[0] = (_player.transform.position + transform.position) / 2;
		spawnPositions[0].y = 0;

		Vector3 toPlayerVector = (transform.position - _player.transform.position).normalized;
		Vector3 sideVector = Vector3.Cross(toPlayerVector, Vector3.up);

		spawnPositions[1] = transform.position + sideVector * _enemySideDistance;
		spawnPositions[2] = transform.position + -sideVector * _enemySideDistance;
		spawnPositions[1].y = 0;
		spawnPositions[2].y = 0;

		StartCoroutine(SpawnEnemies(spawnPositions, machine));
		_animationHandler.SetAnimation("t_Stomp");
		_blendshapeHandler.SetBlendshape("Flint_face.eyes_angry", 90f);
		_blendshapeHandler.SetBlendshape("Flint_face.angry", 90f);
		_blendshapeHandler.SetBlendshape("Flint_face.closed_mouth", 70f);

	}

	public override void ExitState(StateMachine machine)
	{
		_blendshapeHandler.SetBlendshape("Flint_face.eyes_angry", 0f);
		_blendshapeHandler.SetBlendshape("Flint_face.angry", 0f);
		_blendshapeHandler.SetBlendshape("Flint_face.closed_mouth", 0f);
	}

	private IEnumerator SpawnEnemies(Vector3[] spawnPositions, StateMachine machine)
	{
		var spawnedEnemies = new List<BehaviourState>();
		
		for (int i = 0; i < 3; i++)
		{
			var g = Instantiate(enemiesToSpawn[Random.Range(0, enemiesToSpawn.Length)], 
				spawnPositions[i], Quaternion.identity);
			
			g.GetComponent<FieldOfView>().FOVAngle = 360; // nobody can see at 360 degrees you said, Timo? #bullied
			spawnedEnemies.Add(g.GetComponent<BehaviourState>());
			
			yield return new WaitForSeconds(_timeBetweenSpawn);
		}
		
		// after the enemies are spawned, go into the hide state.
		_h.SetSpawnedEnemies(spawnedEnemies.ToArray());
		machine.SwitchState("HideAfterSpawnState");
	}

	public override void Act(StateMachine machine) { }

	public override void Reason(StateMachine machine)
	{
		// no reasoning needed because flint is a lil' bitch, he ain't reasonin' with sap 
	}
}
