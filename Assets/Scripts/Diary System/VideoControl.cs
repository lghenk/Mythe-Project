using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Video;

/// <summary author="Timo Heijne">
/// This controls the video player
/// </summary>
[RequireComponent(typeof(VideoPlayer))]
public class VideoControl : MonoBehaviour {

	private MeshRenderer _meshRenderer;
	private VideoPlayer _videoPlayer;
	private AudioSource _audioSource;

	private Vector3 _prevCamPos;
	
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
		_meshRenderer.enabled = true;
		_videoPlayer.clip = video;
		_videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
		_videoPlayer.SetTargetAudioSource(0, _audioSource);
		_videoPlayer.Play();
		Time.timeScale = 0;
		Camera.main.GetComponent<PostProcessLayer>().enabled = false;
		Camera.main.GetComponent<OrbitCamera>().enabled = false;
		_prevCamPos = Camera.main.transform.position;
		Camera.main.transform.position = new Vector3(0, 100, 0);
		
		MusicManager.Instance.Pause();
	}

	public void StopPlaying() {
		_meshRenderer.enabled = false;
		_videoPlayer.Stop();
		Time.timeScale = 1;
		Camera.main.GetComponent<PostProcessLayer>().enabled = true;
		Camera.main.GetComponent<OrbitCamera>().enabled = true;
		MusicManager.Instance.Resume();
		Camera.main.transform.position = _prevCamPos;
	}

	private void Update() {
		if (Input.GetKeyDown(KeyCode.Return)) {
			StopPlaying();
		}
	}
}
