using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary author="Timo Heijne">
/// This object keeps track of the current inventory
/// </summary>
public class Inventory : MonoBehaviour {

	private static Inventory _instance;
	public static Inventory Instance => _instance;

	private Dictionary<ItemObject, int> _currentInventory = new Dictionary<ItemObject, int>();
	public Dictionary<ItemObject, int> CurrentInventory => _currentInventory;

	public Action<ItemObject, int> onItemAdd;
	public Action<ItemObject, int> onItemRemove;
	public Action<ItemObject> onItemDrop;
	public Action<ItemObject> onItemEquip;

	// Use this for initialization
	void Start() {
		if (Inventory.Instance == null) {
			// Yooooo.. this is a new instance
			_instance = this;
		}
	}

	public void AddItem(ItemObject item, int amount = 1) {
		if (CurrentInventory.ContainsKey(item)) {
			// Item already exists. So we can simply add
			CurrentInventory[item] += amount;
		} else {
			// Item does not exist yet... So we create entry
			CurrentInventory.Add(item, amount);
		}
		
		onItemAdd?.Invoke(item, amount);
	}

	public void RemoveItem(ItemObject item, int amount = 1) {
		if (!CurrentInventory.ContainsKey(item)) return; // Item does not exist. So we cannot remove

		CurrentInventory[item] -= amount;
		if (CurrentInventory[item] < 0) CurrentInventory[item] = 0;
		
		onItemRemove?.Invoke(item, amount);
	}

	public void DropItem(ItemObject item) {
		
		onItemDrop?.Invoke(item);
	}

	public void EquipItem(ItemObject item) {
		
		onItemEquip?.Invoke(item);
	}

	public bool HasItem(ItemObject item, int amount = 1) {
		if (CurrentInventory.ContainsKey(item) && CurrentInventory[item] >= amount) return true;
		return false;
	}
	
}
