using UnityEngine;

[CreateAssetMenu(fileName = "New Melee Weapon", menuName = "Weapons/Melee", order = 1)]
public class MeleeType : ScriptableObject {
    [SerializeField]
    private string _name = "Wooden sword";
    public string Name => _name;

    [SerializeField]
    private float _damage = 5;
    public float Damage => _damage;
    
    [SerializeField]
    private float _radius = 2;
    public float Radius => _radius;
}