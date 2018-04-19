using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Created By Timo Heijne
public class AnimationHandler : MonoBehaviour {

    private string _currentAnimParameter;
    
    [SerializeField, Tooltip("If you don't enter this it will simply use the one on the current object")]
    private Animator _animator;

    public Action onAnimationFinish; 

    void Start() {
        if(!_animator) // If there isn't one set already. Try and get the one on this object
            _animator = GetComponent<Animator>();

        if (!_animator) // If there doesn't exist one... whine about it in the console
            throw new Exception("No animator present on object with AnimationHandler Script...");
    }

    public void SetAnimation(string anim, bool booleanValue) {
        _currentAnimParameter = anim;
        _animator?.SetBool(anim, booleanValue);
        if(booleanValue)
            StartCoroutine(AwaitForParamClose());
    }

    public void SetAnimation(string anim, float state) {
        _currentAnimParameter = anim;
        _animator?.SetFloat(anim, state);
    }

    public void SetAnimation(string anim) {
        _currentAnimParameter = anim;
        _animator?.SetTrigger(anim);
        
        StartCoroutine(AwaitForParamClose());
        _animator.GetBool(_currentAnimParameter);
    }

    IEnumerator AwaitForParamClose() { // Yeet
        yield return new WaitUntil(() => _animator.GetBool(_currentAnimParameter) == false);
        onAnimationFinish?.Invoke();
    }
}
