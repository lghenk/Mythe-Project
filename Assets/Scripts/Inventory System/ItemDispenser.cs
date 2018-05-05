using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary author="Timo Heijne">
/// This will be attached to every object that can dispense an item to the player
/// </summary>
public class ItemDispenser : MonoBehaviour {
    [SerializeField] private ItemObject _itemObject;
    [SerializeField] private int _numItems = 2;

    public ItemObject TakeItem() {
        if (_numItems <= 0) return null;
        
        _numItems--;
        return _itemObject;
    }
}