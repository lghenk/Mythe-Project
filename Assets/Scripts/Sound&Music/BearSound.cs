using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearSound : MonoBehaviour
{
    [SerializeField] private AudioClip[] _roarSounds;
    
    [SerializeField] private ChargeAttackState _s1;
    [SerializeField] private StaticAttackState _s2;

    private AudioSource _source;
    
    private void Start()
    {
        if (!_s1) _s1 = GetComponent<ChargeAttackState>();
        if (!_s2) _s2 = GetComponent<StaticAttackState>();
        
        _s1.OnAttack += OnAttack;
        _s2.OnAttack += OnAttack;

        _source = gameObject.AddComponent<AudioSource>();
        _source.spatialBlend = 1.0f;
    }

    private void OnAttack()
    {
        if (_source.isPlaying) return;
        _source.clip = RandomSound(_roarSounds);
        _source.Play();
    }

    private static AudioClip RandomSound(AudioClip[] a)
    {
        return a[UnityEngine.Random.Range(0, a.Length)];
    }
}
