using System;
using System.Linq;
using UnityEngine;

public class DamageTrigger : MonoBehaviour {
    [SerializeField]
    private float _damage = 5;

    public float Damage {
        get { return _damage; }
        set { _damage = value; }
    }

    private bool _enabled;

    public bool Enabled {
        get { return _enabled; }
        set { _enabled = value; }
    }

    [SerializeField]
    private string[] _excludedTags;

    public Action<Collider> onDamageTrigger;

    private void OnTriggerEnter(Collider other) {
        if (!_enabled || _excludedTags.Contains(other.tag))
            return;
        
        other.GetComponent<Health>()?.TakeDamage(_damage);
        onDamageTrigger?.Invoke(other);
    }
}