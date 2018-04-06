using System.Linq;
using UnityEngine;

public class DamageTrigger : MonoBehaviour {
    [SerializeField]
    private float _damage = 5;

    private bool _enabled;

    public bool Enabled {
        get { return _enabled; }
        set { _enabled = value; }
    }

    [SerializeField]
    private string[] _excludedTags;

    private void OnTriggerEnter(Collider other) {
        if (_enabled && !_excludedTags.Contains(other.tag)) {
            other.GetComponent<Health>()?.TakeDamage(_damage);
            print(other.GetComponent<Health>()?.CurHealth);
        }
    }
}