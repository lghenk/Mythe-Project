using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

/// <summary author="Timo Heijne">
/// This controls the video player
/// </summary>
[RequireComponent(typeof(VideoPlayer))]
public class VideoControl : MonoBehaviour {

	private MeshRenderer _meshRenderer;
	private VideoPlayer _videoPlayer;
	private AudioSource _audioSource;
	
	// Use this for initialization
	void Start () {
		_videoPlayer = GetComponent<VideoPlayer>();
		_meshRenderer = GetComponent<MeshRenderer>();
		_audioSource = GetComponent<AudioSource>();
		_videoPlayer.loopPointReached += VideoPlayerOnLoopPointReached;
	}

	private void VideoPlayerOnLoopPointReached(VideoPlayer source) {
		// Means the video has finished we can hide the plane now
		StopPlaying();
	}

	public void PlayVideo(VideoClip video) {
		print("Should Start Playin boi");
		_meshRenderer.enabled = true;
		_videoPlayer.clip = video;
		_videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
		_videoPlayer.SetTargetAudioSource(0, _audioSource);
		_videoPlayer.Play();
	}

	public void StopPlaying() {
		_meshRenderer.enabled = false;
		_videoPlayer.Stop();
	}
}
