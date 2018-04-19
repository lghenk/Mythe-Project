using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary author="Timo Heijne">
/// This script will pickups to add to () the inventory
/// </summary>
public class PlayerPickupDetection : MonoBehaviour {
	
	// TODO: Handle hit detection for trees and rocks
	
	private void OnTriggerEnter(Collider other) {
		Pickupable pickup = other.GetComponent<Pickupable>();
		if (pickup && pickup.itemObject != null) {
			Inventory.Instance.AddItem(pickup.itemObject);
			Destroy(other.gameObject); // Remove ground model
		}
	}

	private void OnCollisionEnter(Collision other) {
		Pickupable pickup = other.gameObject.GetComponent<Pickupable>();
		if (pickup && pickup.itemObject != null) {
			print("Test");

			Inventory.Instance.AddItem(pickup.itemObject);
			Destroy(other.gameObject); // Remove ground model
		}
	}
}
