using UnityEngine;

/// <summary>
/// Made by Koen Sparreboom
/// </summary>
[CreateAssetMenu(fileName = "New Melee Weapon", menuName = "Weapons/Melee", order = 1), System.Serializable]
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