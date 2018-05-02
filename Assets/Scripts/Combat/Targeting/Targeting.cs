using UnityEngine;

/// <summary author="Koen Sparreboom">
/// Targeting
/// </summary>
public class Targeting : MonoBehaviour {
    // Camera variables
    [SerializeField]
    private bool _autoTargetCamera = true;
    
    [SerializeField]
    private Camera _camera;

    // Settings variables
    [SerializeField]
    private float _maxTargetingDistance = 10;


    // Target variables
    private Transform _target;

    public Transform Target => _target;

    private void Start() {
        if (_autoTargetCamera) {
            _camera = Camera.main;
        }
    }

    private void Update() {
        // Stop targeting if the target is too far
        if (_target && Vector3.Distance(_camera.transform.position, _target.transform.position) > _maxTargetingDistance) {
            _target = null;
        }
        
        // Return if the target button hasn't been pressed
        if (!Input.GetButtonDown("Fire2")) {
            return;
        }

        if (!_target) {
            // Check if an enemy can be targeted and if so which enemy it should be
            int lowestIndex = -1;

            for (int i = 0; i < EnemyTracker.activeEnemies.Count; i++) {
                Debug.Log(_camera.ViewportToWorldPoint(EnemyTracker.activeEnemies[i].transform.position).magnitude);

                if (_camera.IsColliderInView(EnemyTracker.activeEnemies[i].GetComponent<Collider>())
                    && Vector3.Distance(_camera.transform.position, EnemyTracker.activeEnemies[i].transform.position) <
                    _maxTargetingDistance) {
                    if (lowestIndex == -1
                        || (_camera.WorldToViewportPoint(EnemyTracker.activeEnemies[i].transform.position) -
                            new Vector3(.5f, .5f)).magnitude <
                        (_camera.WorldToViewportPoint(EnemyTracker.activeEnemies[lowestIndex].transform.position) -
                         new Vector3(.5f, .5f)).magnitude) {
                        lowestIndex = i;
                    }
                }
            }

            if (lowestIndex != -1) {
                _target = EnemyTracker.activeEnemies[lowestIndex].transform;
            }
            else {
                _target = null;
            }
        }
        else {
            // Stop targeting
            _target = null;
        }
    }
}