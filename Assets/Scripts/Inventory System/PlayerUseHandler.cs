using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

/// <summary author="Timo Heijne">
/// This script handles the effect of usage of a usable item.
/// </summary>
public class PlayerUseHandler : EquipListener {
    protected override void OnItemEquip(ItemObject itemObject) { 
        print("Equip... 2");

        if (itemObject.Type == ItemObject.ItemType.Usable) {
            var assembly = Assembly.GetExecutingAssembly();

            var type = assembly.GetTypes()
                .First(t => t.Name == itemObject.UseBehaviour);

            if (type == null) {
                throw new Exception($"Use Behaviour Class \"{itemObject.UseBehaviour}\" Not Found!");
            }

            // Unity will complain about this, however this is the best and loosly coupled method i could find that works
            // You CAN NOT add monobehaviour or any other script types to a ScriptableObject. otherwise that would've been the solution
            // so this'll do
            UseBehaviour ub = (UseBehaviour)Activator.CreateInstance(type);
            ub.Execute(this.gameObject);
            Inventory.Instance.RemoveItem(itemObject);
        }
    }
}
