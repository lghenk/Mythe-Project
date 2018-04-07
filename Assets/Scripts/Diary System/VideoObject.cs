using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

/// <summary author="Timo Heijne">
/// This script creates the Object so we can add new video objects from the asset menu. Makes it ez to add more yo
/// </summary>
[CreateAssetMenu(menuName = "Custom Objects/Video Object", order = 1)]
public class VideoObject : ScriptableObject {
	public string videoName;
	public VideoClip clip;
}
