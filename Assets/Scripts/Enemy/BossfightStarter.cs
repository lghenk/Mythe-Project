using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BossfightStarter : MonoBehaviour
{
    [SerializeField] private float _startOnDistance;
    [SerializeField] private StateMachine _flint;
    [SerializeField] private LookAtOnState _flint2;
    
    private GameObject Player;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    
    private void FixedUpdate()
    {
        if (!Application.isPlaying) return;

        float distance = (Player.transform.position - transform.position).sqrMagnitude;

        if (distance >= _startOnDistance) return;
        
        // remove all enemies

        foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Destroy(enemy);
        }
        
        // todo : enable a invisible wall
        // todo : place the player a bit more forward (so that he can't get stuck in the invisible wall?)
        
        // enable Flint.
        _flint.enabled = true;
        _flint2.enabled = true;
        
        // Destroy this!
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0.8f, 0.0f, 0.8f, 0.2f);
        Gizmos.DrawSphere(transform.position, Mathf.Sqrt(_startOnDistance));
    }
}
