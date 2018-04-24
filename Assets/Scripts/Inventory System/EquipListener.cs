using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary author="Timo Heijne">
/// This would be added to all the scripts that want to listen to on item equip messages
/// </summary>
public abstract class EquipListener : MonoBehaviour {
	protected GameObject player;
	
	private void Start() {
		player = GameObject.FindGameObjectWithTag("Player");
		if(player == null) 
			Debug.LogError("No player was found in the scene");
		
		if(Inventory.Instance)
			Inventory.Instance.onItemEquip += OnItemEquip;
	}

	protected virtual void OnItemEquip(ItemObject itemObject) {
		throw new NotImplementedException();
	}
}
