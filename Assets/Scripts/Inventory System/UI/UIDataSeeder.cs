using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDataSeeder : MonoBehaviour {

    [SerializeField] private Text _itemName;
    [SerializeField] private Text _itemDescription;
    [SerializeField] private Image _itemIcon;
    [SerializeField] private Text _itemAmount;
    [SerializeField] private Button _craftButton;
    [SerializeField] private Button _equipButton;
    [SerializeField] private Button _dropButton;
    
    public void SetItem(ItemObject itemObject) {
        _itemName.text = itemObject.ItemName;
        _itemDescription.text = itemObject.ItemDescription;
        _itemIcon.sprite = itemObject.ItemIcon;
        
    }

    public void SetCraftable() {
        throw new NotImplementedException();
    }
}
