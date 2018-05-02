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
    
    [SerializeField] 
    private float _attackCooldown = 5;

    private GameObject _currentWeapon;

    private float _lastAttack;

    protected void Start() {
        base.Start();
    }

    public void Attack() {
        if (_meleeType == null || Time.timeSinceLevelLoad - _lastAttack < _attackCooldown) return;
        
        Vector3 center = transform.position + new Vector3(0, 1.25f, 0);
     
        Ray ray;
        RaycastHit[] hits = Physics.SphereCastAll(center + (transform.forward * 1.25f), 1.2f, transform.forward, Mathf.Infinity,  _collisionLayers.value);

        foreach (var hit in hits) {
            hit.transform.GetComponent<Health>()?.TakeDamage(_meleeType.Damage);
        }

        _lastAttack = Time.timeSinceLevelLoad;
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

    private void OnDrawGizmos() {
        Vector3 center = transform.position + new Vector3(0, 1.25f, 0);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(center + (transform.forward * 1.25f), 1.2f);
    }
}