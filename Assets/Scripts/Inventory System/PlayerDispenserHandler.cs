using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary author="Timo Heijne">
/// This class handles the dispenser on a hit of a weapon
/// </summary>
public class PlayerDispenserHandler : MonoBehaviour {

	private MeleeCombat _meleeCombat;
	
	// Use this for initialization
	void Start () {
		_meleeCombat = GetComponent<MeleeCombat>();
		_meleeCombat.onHit += OnHit;
	}

	private void OnHit(Transform transform) {
		ItemDispenser id = transform.GetComponent<ItemDispenser>();
		if (id != null) {
			ItemObject io = id.TakeItem();			
			Inventory.Instance.AddItem(io);
		}
	}
}
