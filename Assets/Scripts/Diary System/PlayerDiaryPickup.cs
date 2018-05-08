using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDiaryPickup : MonoBehaviour {
	private void OnTriggerEnter(Collider other) {
		if (!other.CompareTag("Diary Pickup")) return;
		DiaryEntryPlaylist.instance.PlayNext();
		
		Destroy(other.gameObject);
	}
}
