using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Video;


/// <summary author="Timo Heijne">
/// Handles all the diary items, wil also send messages to the videocontrol to play certain videos...
/// </summary>
[RequireComponent(typeof(VideoControl)), ExecuteInEditMode]
public class DiaryBookkeeper : MonoBehaviour {

	public static DiaryBookkeeper instance;
	[HideInInspector] public VideoObject[] diaryItems; // Would prefer this to be a dictionary but whatever...
	private VideoControl _videoControl;
	public VideoControl VideoControl => _videoControl;

	private void Start() {
		if (instance == null) {
			instance = this;
			_videoControl = GetComponent<VideoControl>();
			
			diaryItems = Resources.FindObjectsOfTypeAll<VideoObject>();
		} else {
			Debug.LogError("DiaryBookkeeper already exists. DELET");
			Destroy(this);
		}
	}

	public bool PlayByName(string name) {
		VideoClip item;
		if (GetByName(name, out item)) {
			// Can start video cuz we found sum clip BOI
			print(item);
			print("Stepper loves weewee");
			_videoControl?.PlayVideo(item);
			return true;
		}

		return false; // Cannot start video
	}

	public void StopVideo() {
		_videoControl?.StopPlaying();
	}

	private bool GetByName(string name, out VideoClip returnItem) {
		VideoObject diaryItem = diaryItems.First(item => item.videoName.Equals(name));
		if (diaryItem != null) {
			returnItem = diaryItem.clip;
			return true;
		} 
		
		returnItem = null;
		return false;
	}
}