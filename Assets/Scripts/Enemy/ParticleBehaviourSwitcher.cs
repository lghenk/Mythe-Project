using System;
using UnityEngine;

public class ParticleBehaviourSwitcher : MonoBehaviour {

    [SerializeField] private ParticleSystem[] _friendlyParticles;
    [SerializeField] private ParticleSystem[] _hostileParticles;

    private BehaviourState _behaviourState;
    
    private void Start() {
        _behaviourState = GetComponent<BehaviourState>();
        _behaviourState.onStateChange += OnStateChange;
    }

    private void OnStateChange(BehaviourState.BehaviourStates behaviourStates) {
        if (behaviourStates == BehaviourState.BehaviourStates.Friendly) {
            SetState(_friendlyParticles, true);
            SetState(_hostileParticles, false);
        } else if (behaviourStates == BehaviourState.BehaviourStates.Hostile) {
            SetState(_friendlyParticles, false);
            SetState(_hostileParticles, true);
        }
    }

    private void SetState(ParticleSystem[] ps, bool enabled) {
        foreach (var p in ps) {
            if (enabled) {
                p.Play();
            } else {
                p.Stop();
            }
        }    
    }
}