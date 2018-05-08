using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthObject : MonoBehaviour {

    [SerializeField, Tooltip("The amount of health this object can spit out")] 
    private float _currentHealth = 100;
    public float CurrentHealth => _currentHealth;

    public float TakeHealth(float amount = -1) {
        if (amount == -1) {
            _currentHealth = 0;
            return _currentHealth;
        } else {
            if (_currentHealth - amount >= 0) {
                _currentHealth -= amount;
                return amount;
            } else {
                _currentHealth = 0;
                return _currentHealth;
            }
        }
    }
}
