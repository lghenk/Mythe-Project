using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary author="Timo Heijne">
/// This will be attached to every object that can dispense an item to the player
/// </summary>
public class ItemDispenser : MonoBehaviour {
    [SerializeField] private DispenserItem[] _items;

    public ItemObject TakeItem() {
        List<int> dis = new List<int>();

        for (int i = 0; i < _items.Length; i++) {
            if(_items[i]._numItems > 0)
                dis.Add(i);
        }

        if (dis.Count == 0) return null;
        int it = Random.Range(0, dis.Count);

        _items[it]._numItems -= 1;
        
        return _items[it]._itemObject;
    }
}