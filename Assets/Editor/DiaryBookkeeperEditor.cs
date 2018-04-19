using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DiaryBookkeeper))]
public class DiaryBookkeeperEditor : Editor {
	private int _selected = 0;
	private readonly List<string> _options = new List<string>();

	public override void OnInspectorGUI() {
		DrawDefaultInspector();
		
		DiaryBookkeeper myScript = (DiaryBookkeeper)target;

		if (!Application.isPlaying) return;

		foreach (var item in myScript.diaryItems) {
			_options.Add(item.videoName);
		}
		
		GUILayout.Space(20);
		EditorGUILayout.LabelField("Debug Options", EditorStyles.boldLabel);
		_selected = EditorGUILayout.Popup("Clip Name:", _selected, _options.ToArray());
		
		if(GUILayout.Button("Test Play")) {
			myScript.PlayByName(_options[_selected]);
		}
		
		if(GUILayout.Button("Stop")) {
			myScript.StopVideo();
		}
	}
}
