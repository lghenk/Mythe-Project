using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DiaryEntryPlaylist : MonoBehaviour {

	public static DiaryEntryPlaylist instance;
	
	[SerializeField] 
	private VideoObject[] _videoObject;
	private List<VideoObject> _playlist;

	[SerializeField]
	private GameObject[] _diaryPickups;
	
	// Use this for initialization
	void Start () {
		instance = this;
		_playlist = _videoObject.ToList();

		foreach (var dp in _diaryPickups) {
			dp.tag = "Diary Pickup";
		}
	}

	public void PlayNext() {
		if (_playlist.Count == 0) return;
		VideoObject entry = _playlist[0];
		_playlist.RemoveAt(0);

		DiaryBookkeeper.instance.PlayByName(entry.videoName);

		if (_diaryPickups.Length == 0) {
			foreach (var dp in _diaryPickups) {
				if (dp)
					Destroy(dp);
			}
		}
	}
}
