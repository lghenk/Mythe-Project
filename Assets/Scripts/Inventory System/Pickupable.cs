using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary author="Timo Heijne">
/// This script handles the settings for a world object that the player can pickup
/// </summary>
public class Pickupable : MonoBehaviour {

    public ItemObject itemObject;

    private void Start() {
        Collider col = GetComponent<Collider>();
        if (!col || itemObject == null) {
            Debug.LogError(gameObject.name + " is a pickupable and not properly configured... deleting");
            Destroy(gameObject);
            return;
        }

        col.isTrigger = true;
    }
}
