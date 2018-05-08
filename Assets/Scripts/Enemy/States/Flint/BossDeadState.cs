using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeadState : State {

	[SerializeField]
	private float _badEndingKillsPercentage = 51;

	private void Start() {
		stateName = "BossDeadState";
	}

	public override void EnterState(StateMachine machine) {
		float percentage = 100 / PlayerKillCounter.instance.totalEnemies * PlayerKillCounter.instance.Kills;

		if (percentage >= _badEndingKillsPercentage) {
			DiaryBookkeeper.instance.PlayByName("Cutscene Bad");
		} else {
			DiaryBookkeeper.instance.PlayByName("Cutscene Good");
		}
	}

	public override void Act(StateMachine machine) { }

	public override void Reason(StateMachine machine) { }
}
