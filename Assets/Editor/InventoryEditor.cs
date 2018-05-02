using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Inventory))]
public class InventoryEditor : Editor {
	
	private int _selected = 0;
	private readonly List<ItemObject> _itemObjects = new List<ItemObject>();
	private readonly List<string> _options = new List<string>();

	private void OnEnable() {
		LoadItems();
	}

	public override void OnInspectorGUI() {
		DrawDefaultInspector();
		
		Inventory myScript = (Inventory)target;

		if (!Application.isPlaying) return;

		foreach (var item in _itemObjects) {
			_options.Add(item.ItemName);
		}
		
		GUILayout.Space(20);
		EditorGUILayout.LabelField("Debug Options", EditorStyles.boldLabel);
		_selected = EditorGUILayout.Popup("Item:", _selected, _options.ToArray());
		
		if(GUILayout.Button("Add To Inventory")) {
			myScript.AddItem(_itemObjects[_selected]);
		}
	
		if(GUILayout.Button("Refresh Item List")) {
			LoadItems();
		}
	}

	private void LoadItems() {
		_itemObjects.Clear();
		_options.Clear();
		
		string[] itemObjects = AssetDatabase.FindAssets("t:ItemObject");
		foreach (var obj in itemObjects) {
			ItemObject io = (ItemObject)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(obj), typeof(ItemObject));
			_itemObjects.Add(io);
		}
		
		foreach (var item in _itemObjects) { 
			_options.Add(item.ItemName);
		}
	}
}
