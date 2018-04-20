using UnityEngine;

/// <summary>
/// Made by Koen Sparreboom
/// </summary>
public class MeleeCombat : EquipListener {
    [SerializeField]
    private MeleeType _meleeType;

    [SerializeField]
    private DamageTrigger _damageTrigger;

    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private string _animatorAttackState = "Attack";

    [SerializeField] 
    private GameObject _weaponPoint;

    private GameObject _currentWeapon;

    private void Start() {
        _damageTrigger.Damage = _meleeType.Damage;
    }

    private void Update() {
        _damageTrigger.Enabled = _animator.GetCurrentAnimatorStateInfo(0).IsName(_animatorAttackState);
    }

    public void ChangeWeapon(MeleeType meleeType) {
        _damageTrigger = _currentWeapon.GetComponent<DamageTrigger>();
        
        _meleeType = meleeType;
        _damageTrigger.Damage = _meleeType.Damage;
    }

    protected override void OnItemEquip(ItemObject itemObject) {
        if (itemObject.Type == ItemObject.ItemType.Weapon) {
            ChangeWeapon(itemObject.MeleeType);

            _currentWeapon = Instantiate(itemObject.GameObject, _weaponPoint.transform);
        }
    }
}