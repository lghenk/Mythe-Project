using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnArea : MonoBehaviour {
    [SerializeField] 
    private GameObject[] _enemies;
    private bool _playedIntro = false;
    private bool _playedFollowup = false;
    
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Spawn Area") && !_playedIntro) {
            _playedIntro = true;
            DiaryBookkeeper.instance.PlayByName("Cutscene Intro");
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Spawn Area") && !_playedFollowup) {
            _playedFollowup = true;
            DiaryBookkeeper.instance.PlayByName("Cutscene Followup");

            foreach (var enemy in _enemies) {
                enemy.SetActive(true);
            }

            PlayerKillCounter.instance.totalEnemies = _enemies.Length;
        }
    }
}
