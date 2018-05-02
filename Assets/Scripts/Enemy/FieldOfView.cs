using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Created By: Timo Heijne
public class FieldOfView : MonoBehaviour {
    private List<Transform> _itemsInView = new List<Transform>();
    public List<Transform> ItemsInView => _itemsInView;

    [SerializeField, Tooltip("The radius around the object that specifies the Field of view range")]
    private float _fovRadius = 50;

    [SerializeField, Tooltip("The angle that the object can see at (no one can see 360 degrees)"), Range(0, 360)]
    private float _fovAngle = 90;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    public Action<Transform> onDetectPlayer;

    // Update is called once per frame
    void Update() {
        FindVisibleTargets();
    }

    void FindVisibleTargets() {
        List<Transform> oldList = ItemsInView;
        _itemsInView.Clear();
        
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, _fovRadius, targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++) {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < _fovAngle / 2) {
                float distToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, dirToTarget, distToTarget, obstacleMask)) {
                    _itemsInView.Add(target);
                }
            }
        }

        foreach (var newItem in _itemsInView) {
            onDetectPlayer?.Invoke(newItem);
        }
    }

    public bool IsInView(Transform trans) {
        return (_itemsInView.IndexOf(trans) != -1);
    }

    public bool HasPlayerInRange() {
        return (_itemsInView.Count > 0);
    }

    private void OnDrawGizmos() {
        foreach (Transform item in _itemsInView) {
            Gizmos.DrawLine(transform.position, item.position);
        }
    }
}