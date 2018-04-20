using System;
using UnityEngine;

/// <summary>
/// Made by Koen Sparreboom
/// </summary>
public class MeleeCombat : EquipListener {
    [SerializeField]
    private MeleeType _meleeType;

    [SerializeField] 
    private LayerMask _collisionLayers;

    [SerializeField] 
    private GameObject _weaponPoint;

    private GameObject _currentWeapon;

    protected void Start() {
        base.Start();
        //_damageTrigger.Damage = _meleeType.Damage;
    }

    public void Attack() {
        if (_meleeType == null) return;
        
        Ray ray;
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, 2, Vector3.forward, Mathf.Infinity,  _collisionLayers.value);

        foreach (var hit in hits) {
            print(hit.transform.name);
            hit.transform.GetComponent<Health>()?.TakeDamage(_meleeType.Damage);
        }
    }

    public void ChangeWeapon(MeleeType meleeType) {
        _meleeType = meleeType;
    }

    protected override void OnItemEquip(ItemObject itemObject) {
        if (itemObject.Type == ItemObject.ItemType.Weapon) {
            if(_currentWeapon)
                Destroy(_currentWeapon);
            
            _currentWeapon = Instantiate(itemObject.GameObject, _weaponPoint.transform);
            ChangeWeapon(itemObject.MeleeType);
        }
    }
}