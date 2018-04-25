using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary author="Timo Heijne">
/// This would be added to all the scripts that want to listen to on item equip messages
/// </summary>
public abstract class EquipListener : MonoBehaviour {
	protected GameObject player;

	protected Inventory inventory;
	
	protected virtual void Start() {
		player = GameObject.FindGameObjectWithTag("Player");
		if(player == null) 
			Debug.LogError("No player was found in the scene");

		print("TEst");

		inventory = player.GetComponent<Inventory>(); 
		inventory.onItemEquip += OnItemEquip;
	}

	protected virtual void OnItemEquip(ItemObject itemObject) {
		throw new NotImplementedException();
	}
}
