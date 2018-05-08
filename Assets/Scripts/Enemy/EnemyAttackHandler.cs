using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary author="Timo Heijne">
/// This will deal damage to any object when the enemenemenyie decides to attack
/// </summary>
public class EnemyAttackHandler : MonoBehaviour {

	[SerializeField]
	private float _range = 2;
	
	[SerializeField]
	private Vector3 _offset;

	[SerializeField] private float _damage = 20;
	
	[SerializeField] 
	private LayerMask _collisionLayers;

	public void Attack() {
		RaycastHit[] hits = Physics.SphereCastAll(transform.position + _offset, _range, transform.forward, Mathf.Infinity,  _collisionLayers.value);
		foreach (var hit in hits) {
			if(hit.collider.gameObject == gameObject) continue;
			
			Debug.Log($"{transform.name} > hit > {hit.collider.name}");
			hit.collider.gameObject.GetComponent<Health>()?.TakeDamage(_damage);
		}
	}

	private void OnDrawGizmosSelected() {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position + _offset, _range);
	}
}
