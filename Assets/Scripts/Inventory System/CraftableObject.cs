using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

/// <summary author="Timo Heijne">
/// Stores the crafting recepies
/// </summary>
[CreateAssetMenu(menuName = "Custom Objects/Craftable Object", order = 1), System.Serializable]
public class CraftableObject : ScriptableObject {

    [SerializeField]
    private bool _isEnabled = true;
    public bool IsEnabled => _isEnabled;

    [SerializeField] 
    private ItemObject _itemObject; 
    public ItemObject ItemObject => _itemObject;

    // You cannot save lists in a SerializableObject unfortunately
    [SerializeField]
    private ReqItems[] _requiredItems = new ReqItems[0];
    public ReqItems[] RequiredItems => _requiredItems;

    public bool IsValidRecipe() {
        return (_itemObject != null);
    }

    public void AddRequiredItem(ItemObject item, int amount = 1) {
        if (amount <= 0) return;
        
        List<ReqItems> requiredItemsList = RequiredItems.ToList(); // for ease of editing convert it to a list
        
        ReqItems cobj = requiredItemsList.FirstOrDefault(citem => citem.obj == item);
        if (cobj != null) {
            // Object already exists!
            int i = requiredItemsList.IndexOf(cobj);
            cobj.amount += amount;
                
            requiredItemsList[i] = cobj;
        } else {
            // Object does not exist yet!
            ReqItems newItem = new ReqItems(item, amount);
            requiredItemsList.Add(newItem);
        }
        
        _requiredItems = requiredItemsList.ToArray(); // Now convert back to array. So it can save
    }

    public void RemoveRequiredItem(ItemObject item, int amount = 1) {
        if (amount <= 0) return;

        List<ReqItems> requiredItemsList = RequiredItems.ToList(); // for ease of editing convert it to a list
        
        ReqItems cobj = requiredItemsList.FirstOrDefault(citem => citem.obj == item);
        if (cobj == null) return;
         
        int i = requiredItemsList.IndexOf(cobj);

        if (amount == cobj.amount) {
            requiredItemsList.Remove(cobj);
        } else {
            cobj.amount -= amount;
            requiredItemsList[i] = cobj;
        }

        _requiredItems = requiredItemsList.ToArray(); // Now convert back to array. So it can save
    }
}

[System.Serializable]
public class ReqItems {
    public ItemObject obj;
    public int amount;
        
    public ReqItems(ItemObject obj, int amount) {
        this.obj = obj;
        this.amount = amount;
    }      
}