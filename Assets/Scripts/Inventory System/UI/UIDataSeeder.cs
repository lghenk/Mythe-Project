using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIDataSeeder : MonoBehaviour {

    private ItemObject _itemObject;
    private CraftableObject _craftableObject;

    [SerializeField] private Text _itemName;
    [SerializeField] private Text _itemDescription;
    [SerializeField] private Image _itemIcon;
    [SerializeField] private Text _itemAmount;
    [SerializeField] private Button _craftButton;
    [SerializeField] private Button _equipButton;
    [SerializeField] private Button _dropButton;
    
    public void SetItem(ItemObject itemObject) {
        _itemObject = itemObject;
        _itemName.text = itemObject.ItemName;
        _itemDescription.text = itemObject.ItemDescription;
        _itemIcon.sprite = itemObject.ItemIcon;
        _itemAmount.text = Inventory.Instance.CurrentInventory[itemObject].ToString();

        if (itemObject.Type == ItemObject.ItemType.CraftingComponent) {
            // Remove Equip Button
            Destroy(_equipButton.gameObject);
        } else if (itemObject.Type == ItemObject.ItemType.Usable) {
            _equipButton.transform.GetChild(0).GetComponent<Text>().text = "Use";
        }
        
        _dropButton?.onClick.AddListener(OnDropClick);
        _equipButton?.onClick.AddListener(OnEquipClick);
    }

    public void SetCraftable(CraftableObject craftObject) {
        _craftableObject = craftObject;
        _itemName.text = craftObject.ItemObject.ItemName;
        _itemDescription.text = craftObject.ItemObject.ItemDescription;
        _itemIcon.sprite = craftObject.ItemObject.ItemIcon;

        List<int> canCraftAmount = new List<int>();
        foreach (var requiredItem in craftObject.RequiredItems) {
            int amount = (int)Math.Floor((decimal)Inventory.Instance.CurrentInventory[requiredItem.obj] / (decimal)requiredItem.amount);
            canCraftAmount.Add(amount);
        }

        _itemAmount.text = (canCraftAmount.Count > 0) ? canCraftAmount.Min(item => item).ToString() : "0";
        _craftButton?.onClick.AddListener(OnCraftClick);
    }
    
    private void OnEquipClick() {
        Inventory.Instance.EquipItem(_itemObject);
    }

    private void OnCraftClick() {
        Inventory.Instance.HandleCraftingRecipe(_craftableObject);
    }

    private void OnDropClick() {
        Inventory.Instance.DropItem(_itemObject);
    }
}
