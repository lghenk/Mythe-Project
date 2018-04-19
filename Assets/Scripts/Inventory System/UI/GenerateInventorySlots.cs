using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateInventorySlots : MonoBehaviour {

	[SerializeField]
	private GameObject _listItemPrefab;
	
	void Start () {
		StartCoroutine(WaitForInventory());
	}

	IEnumerator WaitForInventory() {
		yield return new WaitUntil(() => Inventory.Instance != null);
		if(Inventory.Instance == null) throw new Exception("Cannot initialize Generate Inventory Slots... No inventory exists");
		Inventory.Instance.onItemAdd += OnItemAdd;
		Inventory.Instance.onItemRemove += OnItemRemove;
		
		DrawItems();
	}

	private void OnItemRemove(ItemObject itemObject, int i) {
		DrawItems();
	}

	private void OnItemAdd(ItemObject itemObject, int i) {
		DrawItems();
	}

	private void DrawItems() {
		if(_listItemPrefab == null) throw new Exception("GenerateInventorySlots :: Unable to draw... No List Item Prefab selected");

		DestroyAllChildObjects();
		
		// Not the most effecient way of handeling inventory slots... but the easiest :P
		foreach (var invItem in Inventory.Instance.CurrentInventory) { // TODO: Put this in a co-routine to avoid blocking (incase of large inventories)
			if(invItem.Value == 0 || !invItem.Key.IsEnabled) continue;
			
			GameObject go = Instantiate(_listItemPrefab, transform);
			UIDataSeeder ids = go.GetComponent<UIDataSeeder>();
			ids.SetItem(invItem.Key);
		}
	}

	private void DestroyAllChildObjects() {
		foreach (Transform child in transform) {
			Destroy(child.gameObject);
		}
	}
}
