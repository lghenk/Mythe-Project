using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary author="Antonio Bottelier">
///
/// Singleton class that handles all music that can be played.
/// 
/// </summary>
public class MusicManager : MonoBehaviour
{
	/**************** STATIC METHODS AND VARIABLES ****************/
	
	private static MusicManager _instance;
	
	public static MusicManager Instance
	{
		get
		{
			if (_instance == null) _instance = Instantiate(Resources.Load<GameObject>("MusicManager"))
				.GetComponent<MusicManager>();
			return _instance;
		}
	}

	public static Action SongFinished;
	public static Action SongInterrupted;

	public static void PlayMainMenuSong() =>
		Instance.PlaySongType(SongType.MainMenu);		

	public static void PlayOverworldSong() =>
		Instance.PlaySongType(SongType.Overworld);

	public static void PlayBossBattleSong() => 
		Instance.PlaySongType(SongType.Boss);

	public static IEnumerable<Song> GetSongs(SongType type) =>
		Instance._songs.Where(s => s.type == type);

	public static IEnumerable<Song> GetSongs() =>
		Instance._songs;

	public static Song GetSong(string songName) =>
		Instance._songs.FirstOrDefault(x => x.songName == songName);
	
	/**************** INSTANCE METHODS AND VARIABLES ****************/

	[SerializeField] private Song[] _songs;

	private AudioSource _source;
	private SongType? _currentPlayingType;

	private Song lastSong;
	private bool lastPlaying;
	
	[System.Serializable]
	public struct Song
	{
		public string songName;
		public float volume;
		public SongType type;
		public AudioClip clip;
	}
	
	public enum SongType
	{
		Other,
		MainMenu,
		Overworld,
		Boss
	}

	void Awake()
	{
		_source = gameObject.AddComponent<AudioSource>();
		SongFinished += SongFinishedPlaying;
	}

	void Update()
	{
		if (!_source.isPlaying && lastPlaying)
		{
			SongFinished?.Invoke();
			Debug.Log("Song Finished");
		}
		
		lastPlaying = _source.isPlaying;
	}
	
	public void PlaySongType(SongType type)
	{
		var songs = _songs.Where(x => x.songName != lastSong.songName && x.type == type).ToArray();
		Play(songs[UnityEngine.Random.Range(0,songs.Length)]);

		_currentPlayingType = type;
	}

	private void SongFinishedPlaying()
	{
		if(_currentPlayingType != null) 
			PlaySongType((SongType)_currentPlayingType);
	}

	public void Play(Song song)
	{
		_currentPlayingType = null;
		if (_source.isPlaying) SongInterrupted?.Invoke();
		
		_source.clip = song.clip;
		_source.volume = song.volume;
		_source.Play();
		lastSong = song;
	}

	public void Pause() {
		_source.volume = 0;
	}

	public void Resume() {
		if (_currentPlayingType != null) {
			StartCoroutine(GraduallyTurnUpVolume());
		}
	}

	IEnumerator GraduallyTurnUpVolume() {
		while (true) {
			_source.volume = Mathf.Lerp(_source.volume, lastSong.volume, Time.deltaTime * 3);
			yield return new WaitForEndOfFrame();

			if (Math.Abs(_source.volume - lastSong.volume) < 0.01) break;
		}
	}
}
