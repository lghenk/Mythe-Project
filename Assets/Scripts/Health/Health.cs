using System;
using UnityEngine;


/// Created By Timo Heijne
/// <summary>
/// Generic Health Script that we can put on anything we'd like
/// </summary>
public class Health : MonoBehaviour {
    [SerializeField] private float _startingHealth = 100;
    public float StartingHealth => _startingHealth;
    public float CurHealth { get; private set; }

    /// <summary>
    /// An Event that invokes when the Object that has this health script supposedly dies
    /// </summary>
    public Action<Health> onDeath;
    
    /// <summary>
    /// An Event that invokes when the object that has this health script gets sum damage
    /// <param name="damageAmount"></param>
    /// <param name="curHealth"></param>
    /// <param name="startingHealth"></param>
    /// </summary>
    public Action<float, float, float, Health> onDamage;

    public Action<string> onMessage;

    [SerializeField] [Tooltip("Define an overriding health module if the default handling is not wanted (for example Down But Not Out would need different handling)")]
    private HealthBaseModule _healthModule;

    public bool godMode = false;

    void Start() {
        _healthModule?.SetHealthReference(this); // Passalong health script to the module (if exists)
        CurHealth = _startingHealth;
    }

    /// <summary>
    /// A function that substracts health from the current health
    /// </summary>
    /// <param name="amount">The amount of damage it should take</param>
    public void TakeDamage(float amount = 1) {
        if (godMode) return; 
        
        CurHealth -= amount;
        CheckDeath();

        if (_healthModule) {
            _healthModule.OnDamage(amount, CurHealth, _startingHealth);
        } else {
            onDamage?.Invoke(amount, CurHealth, _startingHealth, this);
        }
    }

    /// <summary>
    /// A function that adds health to the current health
    /// </summary>
    /// <param name="amount">The amount of health it should add</param>
    public void AddHealth(float amount = 1) {
        CurHealth += amount;

        if (CurHealth > _startingHealth)
            CurHealth = _startingHealth;
    }

    public void SetHealth(float h) {
        CurHealth = h;
        CheckDeath();
    }

    /// <summary>
    /// Check whether this object is dead or not
    /// </summary>
    /// <returns>A boolean if true object is dead</returns>
    public bool IsDead() {
        if (_healthModule) {
            return _healthModule.IsDead;
        } 
        
        return (CurHealth <= 0);
    }

    public void ResetHealth() {
        CurHealth = _startingHealth;
    }

    private void CheckDeath() {
        if (CurHealth > 0) return;

        if (_healthModule) {
            _healthModule.OnDeath();
        } else {
            onDeath?.Invoke(this);
        }
    }
}