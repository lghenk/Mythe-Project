using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary author="Timo Heijne">
/// This script handles the inspector settings for items
/// </summary>
[CustomEditor(typeof(ItemObject))]
public class ObjectEditor : Editor {

    public SerializedProperty isEnabled, type, meleeType, itemName, itemDescription, itemIcon, gameObject, useBehaviour;
    
    private void OnEnable() {
        isEnabled = serializedObject.FindProperty("_isEnabled");
        type = serializedObject.FindProperty("_type");
        meleeType = serializedObject.FindProperty("_meleeType");
        itemName = serializedObject.FindProperty("_itemName");
        itemDescription = serializedObject.FindProperty("_itemDescription");
        itemIcon = serializedObject.FindProperty("_itemIcon");
        gameObject = serializedObject.FindProperty("_gameObject");
        useBehaviour = serializedObject.FindProperty("_useBehaviour");
    }

    public override void OnInspectorGUI() {
        serializedObject.Update();        

        ItemObject myScript = (ItemObject)target;
        
        EditorGUILayout.LabelField("General Item Properties", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(isEnabled, new GUIContent("Is Enabled"));
        EditorGUILayout.PropertyField(type, new GUIContent("Type"));
        EditorGUILayout.PropertyField(itemName, new GUIContent("Item Name"));
        EditorGUILayout.PropertyField(itemIcon, new GUIContent("Item Icon"));
        EditorGUILayout.PropertyField(gameObject, new GUIContent("Item Prefab"));
        
        EditorGUILayout.LabelField("Item Description");
        itemDescription.stringValue = EditorGUILayout.TextArea(itemDescription.stringValue, GUILayout.Height(75));
        
        EditorGUILayout.Space();
        if (myScript.Type == ItemObject.ItemType.Weapon) {
            
            EditorGUILayout.LabelField("Weapon Item Specific Properties", EditorStyles.boldLabel);

            EditorGUILayout.PropertyField(meleeType, new GUIContent("Melee Type"));
        } else if (myScript.Type == ItemObject.ItemType.Usable) {
            EditorGUILayout.LabelField("Usable Item Specific Properties", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(useBehaviour, new GUIContent("Use Behaviour"));
        }

        serializedObject.ApplyModifiedProperties();
    }
}
