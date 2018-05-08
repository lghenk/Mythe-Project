using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class BossDeadState : State {

	[SerializeField]
	private float _badEndingKillsPercentage = 51;
	
	public int sceneIndex = 0;

	private void Start() {
		stateName = "BossDeadState";
	}

	public override void EnterState(StateMachine machine) {
		float percentage = 100 / PlayerKillCounter.instance.totalEnemies * PlayerKillCounter.instance.Kills;

		if (percentage >= _badEndingKillsPercentage) {
			DiaryBookkeeper.instance.PlayByName("Cutscene Bad");
			DiaryBookkeeper.instance.VideoControl.VideoPlayer.loopPointReached += VideoPlayerOnLoopPointReached;
		} else {
			DiaryBookkeeper.instance.PlayByName("Cutscene Good");
			DiaryBookkeeper.instance.VideoControl.VideoPlayer.loopPointReached += VideoPlayerOnLoopPointReached;
		}
	}

	private void VideoPlayerOnLoopPointReached(VideoPlayer source) {
		SceneManager.LoadScene(sceneIndex);
	}

	public override void Act(StateMachine machine) { }

	public override void Reason(StateMachine machine) { }
}
