using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class GenerateCraftingSlots : MonoBehaviour {

	[SerializeField]
	private GameObject _listItemPrefab;

	private List<CraftableObject> _craftableObjects = new List<CraftableObject>();
	
	// Use this for initialization
	void Start () {
		if(Inventory.Instance == null) throw new Exception("Cannot initialize Generate Crafting Slots... No inventory exists");
		Inventory.Instance.onItemAdd += OnItemAdd;
		Inventory.Instance.onItemRemove += OnItemRemove;
		
		// Load all craftable objects at the beginning of the game
		LoadItems();
		
		// Then draw the initial crafting list. // Likely no items at the begin but can't hurt right?
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
		
		// Calculate what items one can craft.
		List<CraftableObject> craftableItems = new List<CraftableObject>(); // Store all the items this user can craft now here.
		foreach (var craftObject in _craftableObjects) {
			bool canCraft = true;
			
			// Loop through the required items, Cross check with the inventory if player has enough. If not. Set CanCraft to false.
			foreach (var requiredItem in craftObject.RequiredItems) {
				if (!Inventory.Instance.HasItem(requiredItem.obj, requiredItem.amount)) canCraft = false;
			}

			if (canCraft && craftObject.IsValidRecipe() && craftObject.IsEnabled) { 
				// Player can craft this item (before the player can actually craft we will validate once the player actually clicked the button)
				craftableItems.Add(craftObject);
			}
		}

		// Not the most effecient way of handeling inventory slots... but the easiest :P
		foreach (var item in craftableItems) { // TODO: Put this in a co-routine to avoid blocking (incase of large inventories)
			GameObject go = Instantiate(_listItemPrefab, transform);
			UIDataSeeder ids = go.GetComponent<UIDataSeeder>();
			ids.SetCraftable(item);
		}
	}

	private void DestroyAllChildObjects() {
		foreach (Transform child in transform) {
			Destroy(child.gameObject);
		}
	}
	
	private void LoadItems() {
		_craftableObjects.Clear();

		_craftableObjects = Resources.FindObjectsOfTypeAll<CraftableObject>().ToList();
	}

}
