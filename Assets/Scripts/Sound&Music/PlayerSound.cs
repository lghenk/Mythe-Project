using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerSound : MonoBehaviour
{
    [SerializeField] private AudioClip[] _hurtSounds;
    [SerializeField] private AudioClip[] _footstepSounds;
    [SerializeField] private AudioClip[] _swordHitSounds;

    private AudioSource _audioSource;
    private MeleeCombat _meleeCombat;
    private Health _health;

    private void Start()
    {
        _audioSource = gameObject.AddComponent<AudioSource>();
        _audioSource.spatialBlend = 1.0f;

        _health = transform.parent.GetComponent<Health>();
        _meleeCombat = transform.parent.GetComponent<MeleeCombat>();
        _health.onDamage += OnDamage;
        _meleeCombat.onHit += OnHit;
    }

    private void OnHit(Transform transform)
    {
        _audioSource.PlayOneShot(GetRandomSound(_swordHitSounds));
    }

    public void PlayFootstep()
    {
        _audioSource.PlayOneShot(GetRandomSound(_footstepSounds));
    }

    private void OnDamage(float f, float f1, float arg3, Health arg4)
    {
        _audioSource.PlayOneShot(GetRandomSound(_hurtSounds));
    }

    private static AudioClip GetRandomSound(AudioClip[] a)
    {
        return a[Random.Range(0,a.Length)];
    }
}
