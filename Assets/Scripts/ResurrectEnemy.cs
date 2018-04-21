using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResurrectEnemy : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.E)) {
			print("CANCER");
			RaycastHit[] hits = Physics.SphereCastAll(transform.position, 2, Vector3.forward);

			foreach (var hit in hits) {
				print(hit.collider.name);
				DBNOModule dbno = hit.collider.GetComponent<DBNOModule>();
				if (dbno && dbno.IsDbno) {
					print("DBNO found");
					dbno.Resurrect();
				}
			}
		}
	}
}
