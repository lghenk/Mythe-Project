using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotionBehaviour : UseBehaviour {
	public override void Execute(GameObject go) {
		Health h = go.GetComponent<Health>();
		Debug.Log("Test");

		if (!h) return;

		Debug.Log("Test - 2");
		h.AddHealth(20);
		GameObject particle = (GameObject)Instantiate(Resources.Load("Particles/Sapling Heal"), go.transform.position, Quaternion.Euler(new Vector3(-90,0,0)));
		Destroy(particle, 5);
	}
}
