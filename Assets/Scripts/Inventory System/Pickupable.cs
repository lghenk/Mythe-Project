using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary author="Timo Heijne">
/// This script handles the settings for a world object that the player can pickup
/// </summary>
public class Pickupable : MonoBehaviour {

    public ItemObject itemObject;
    public float rotationSpeed = 2.5f;

    private void Start() {
        Collider col = GetComponent<Collider>();
        if (!col || itemObject == null) {
            Debug.LogError(gameObject.name + " is a pickupable and not properly configured... deleting");
            Destroy(gameObject);
            return;
        }

        col.isTrigger = true;
        
        if(transform.childCount > 0)
            transform.GetChild(0).rotation = Quaternion.Euler(new Vector3(-20, 30, -120));
    }

    private void Update() {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, rotationSpeed, 0));
    }
}
