using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthObjectHandler : MonoBehaviour {
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

				GameObject go = (GameObject)Instantiate(Resources.Load("Particles/Sapling Heal"), transform.position, Quaternion.Euler(new Vector3(-90,0,0)));
				Destroy(go, 5);

			}
		}
	}
}
