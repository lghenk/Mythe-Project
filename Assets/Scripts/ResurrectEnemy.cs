using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResurrectEnemy : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.E)) {
			RaycastHit[] hits = Physics.SphereCastAll(transform.position, 2, Vector3.forward);

			foreach (var hit in hits) {
				DBNOModule dbno = hit.collider.GetComponent<DBNOModule>();
				if (dbno && dbno.IsDbno) {
					print("DBNO found");
					dbno.Resurrect();
					
					GameObject go = (GameObject)Instantiate(Resources.Load("Particles/Enemy Heal"), transform.position, Quaternion.Euler(new Vector3(-90,0,0)));
					Destroy(go, 5);
				}
			}
		}
	}
}
