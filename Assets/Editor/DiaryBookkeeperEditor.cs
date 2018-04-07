using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DiaryBookkeeper))]
public class DiaryBookkeeperEditor : Editor {
	private string nameOfClip;
	
	public override void OnInspectorGUI() {
		DrawDefaultInspector();
		
		DiaryBookkeeper myScript = (DiaryBookkeeper)target;

		if (!Application.isPlaying) return;
		
		GUILayout.Space(20);
		EditorGUILayout.LabelField("Debug Options", EditorStyles.boldLabel);
		
		nameOfClip = EditorGUILayout.TextField("Clip Name:", nameOfClip);
		
		if(GUILayout.Button("Test Play")) {
			Debug.Log(nameOfClip);
			myScript.PlayByName(nameOfClip);
		}
		
		if(GUILayout.Button("Stop")) {
			myScript.StopVideo();
		}

	}
}
