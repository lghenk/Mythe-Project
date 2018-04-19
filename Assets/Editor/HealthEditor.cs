using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Health))]
public class HealthEditor : Editor {
    private int _amount;

    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        Health health = (Health) target;

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Debug Shit Yo", EditorStyles.boldLabel);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Current Health");
        EditorGUILayout.LabelField(health.CurHealth.ToString());
        EditorGUILayout.EndHorizontal();
        
        EditorGUILayout.BeginHorizontal();
        _amount = EditorGUILayout.IntField(_amount);
        if (GUILayout.Button("Do Damage")) {
            health.TakeDamage(_amount);
        }
        EditorGUILayout.EndHorizontal();
    }
}