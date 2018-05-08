using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKillCounter : MonoBehaviour {

	public static PlayerKillCounter instance;

	private int kills = 0;
	public int Kills => kills;

	public int totalEnemies = 0;
	
	// Use this for initialization
	void Start () {
		instance = this;
	}

	public void AddKill() {
		kills += 1;
	}
}
