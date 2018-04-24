using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary author="Timo Heijne">
/// Handles the editor of the CraftableObject
/// </summary>
[CustomEditor(typeof(CraftableObject))]
public class CraftableEditor : Editor {
	
	private readonly List<ItemObject> _itemObjects = new List<ItemObject>();
	private readonly List<string> _itemNames = new List<string>();
	private SerializedProperty _isEnabled, _itemObject;

	private int _selectedItem = -1;
	private int _amount = 1;
	
	private void OnEnable() {
		_isEnabled = serializedObject.FindProperty("_isEnabled");
		_itemObject = serializedObject.FindProperty("_itemObject");
		
		RefreshItemListing();		
	}

	public override void OnInspectorGUI() {
		serializedObject.Update();

		CraftableObject craftObject = (CraftableObject)target;
		
		EditorGUILayout.LabelField("General Options", EditorStyles.boldLabel);
		EditorGUILayout.PropertyField(_isEnabled, new GUIContent("Is Enabled"));
		EditorGUILayout.PropertyField(_itemObject, new GUIContent("Item Object"));
		if(craftObject.ItemObject == null) 
			EditorGUILayout.HelpBox("Without an itemobject this recipe wont work!", MessageType.Warning);

		if (craftObject.RequiredItems.Length > 0) {
			EditorGUILayout.Space();
			EditorGUILayout.LabelField("Required Crafting Components", EditorStyles.boldLabel);

			foreach (ReqItems cObj in craftObject.RequiredItems) {
				EditorGUILayout.BeginHorizontal();
				
				EditorGUILayout.LabelField(cObj.amount + "x " + cObj.obj.ItemName);
				if (GUILayout.Button("-"+cObj.amount)) {
					craftObject.RemoveRequiredItem(cObj.obj, cObj.amount);
				}
				
				if (GUILayout.Button("-1")) {
					craftObject.RemoveRequiredItem(cObj.obj, 1);
				}	
				
				if (GUILayout.Button("+1")) {
					craftObject.AddRequiredItem(cObj.obj);
				}
				
				EditorGUILayout.EndHorizontal();
			}
		}
			
		
		EditorGUILayout.Space();
		EditorGUILayout.LabelField("Add Crafting Requirement", EditorStyles.boldLabel);
		
		if(_selectedItem >= 0 && !_itemObjects[_selectedItem].IsEnabled)
			EditorGUILayout.HelpBox("The selected item is disabled!", MessageType.Warning);
		
		if(_amount <= 0) 
			EditorGUILayout.HelpBox("Can not add zero or negative items to the craft requirements", MessageType.Error);
		
		EditorGUILayout.BeginHorizontal();
	
		_selectedItem = EditorGUILayout.Popup(_selectedItem, _itemNames.ToArray());
		_amount = EditorGUILayout.IntField(_amount);
		 
		EditorGUILayout.EndHorizontal();

		if (GUILayout.Button("Add Requirement")) {
			if (!(_amount > 0 && _selectedItem > -1 && _selectedItem < _itemObjects.Count)) return;
			
			craftObject.AddRequiredItem(_itemObjects[_selectedItem], _amount);
		}
		
		EditorGUILayout.Space();
		EditorGUILayout.LabelField("Misc", EditorStyles.boldLabel);
		if (GUILayout.Button("Save")) {
			serializedObject.Update();
			serializedObject.ApplyModifiedProperties();
			EditorUtility.SetDirty(target);
			
			Debug.Log(AssetDatabase.GetAssetPath(target.GetInstanceID()));
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();
		}
		
		if (GUILayout.Button("Refresh Item Objects List")) {
			RefreshItemListing();
		}
		
		serializedObject.ApplyModifiedProperties();
	}

	private void RefreshItemListing() {
		_itemObjects.Clear();
		_itemNames.Clear();
		
		string[] itemObjects = AssetDatabase.FindAssets("t:ItemObject");
		foreach (var obj in itemObjects) {
			ItemObject io = (ItemObject)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(obj), typeof(ItemObject));
			_itemObjects.Add(io);
		}
		
		foreach (var item in _itemObjects) { 
			_itemNames.Add(item.ItemName);
		}
	}
}
