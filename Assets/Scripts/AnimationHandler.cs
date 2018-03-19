using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationHandler : MonoBehaviour {
    private Animator _animator;

    // Use this for initialization
    void Start() {
        _animator = GetComponent<Animator>();

        if (!_animator)
            throw new Exception("No animator present on object with AnimationHandler Script...");
    }

    // Update is called once per frame
    void Update() { }

    public void SetAnimation(string anim, bool booleanValue) {
        _animator?.SetBool(anim, booleanValue);
    }

    public void SetAnimation(string anim, float state) {
        _animator?.SetFloat(anim, state);
    }

    public void SetAnimation(string anim) {
        _animator?.SetTrigger(anim);
    }
}