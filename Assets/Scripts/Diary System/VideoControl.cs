using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class VideoControl : MonoBehaviour {

	private VideoPlayer _videoPlayer;
	
	// Use this for initialization
	void Start () {
		_videoPlayer = GetComponent<VideoPlayer>();
		
		_videoPlayer.loopPointReached += VideoPlayerOnLoopPointReached;
	}

	private void VideoPlayerOnLoopPointReached(VideoPlayer source) {
		// Means the video has finished we can hide the plane now
	}

	// Update is called once per frame
	void Update () {
		
	}
}
