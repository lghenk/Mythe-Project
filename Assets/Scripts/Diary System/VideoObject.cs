using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu(menuName = "Custom Objects/Video Object", order = 1)]
public class VideoObject : ScriptableObject {
	public string videoName;
	public VideoClip clip;
}
