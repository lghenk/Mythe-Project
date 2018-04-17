using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary author="Timo Heijne">
/// This object stores all the items that a player can use
/// </summary>
[CreateAssetMenu(menuName = "Custom Objects/Item Object", order = 1), System.Serializable]
public class ItemObject : ScriptableObject {

    [System.Serializable]
    public enum ItemType {
        Usable,
        Weapon,
        CraftingComponent
    }

    [SerializeField]
    protected ItemType _type = ItemType.Usable;
    public ItemType Type => _type;
    
    [SerializeField]
    private bool _isEnabled = true;
    public bool IsEnabled => _isEnabled;

    [SerializeField]
    private string _itemName;
    public string ItemName => _itemName;
    
    [SerializeField]
    private string _itemDescription;
    public string ItemDescription => _itemDescription;

    [SerializeField]
    private Sprite _itemIcon;
    public Sprite ItemIcon => _itemIcon;
    
    [SerializeField]
    private GameObject _gameObject;
    public GameObject GameObject => _gameObject;

    [SerializeField] 
    private MeleeType _meleeType;
    public MeleeType MeleeType => _meleeType;

    [SerializeField]
    private string _useBehaviour;
    public string UseBehaviour => _useBehaviour;
}
