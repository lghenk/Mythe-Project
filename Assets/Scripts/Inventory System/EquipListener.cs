using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EquipListener : MonoBehaviour {
	private void Start() {
		Inventory.Instance.onItemEquip += OnItemEquip;
	}

	protected virtual void OnItemEquip(ItemObject itemObject) {
		throw new NotImplementedException();
	}
}
