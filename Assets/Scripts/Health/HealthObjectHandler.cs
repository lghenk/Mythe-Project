using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthObjectHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.E)) {
			RaycastHit hit;
			if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 10)) {
				HealthObject healthobj = hit.collider.GetComponent<HealthObject>();
				if (!healthobj) return;
					
				Health health = GetComponent<Health>();
				if (!health) return;

				float required = health.StartingHealth - health.CurHealth;
				float amount = healthobj.TakeHealth(required);
				health.AddHealth(amount);
			}
		}
	}
}
