using System.Collections;
using UnityEngine;

/// <summary author="Koen Sparreboom">
/// Script for sliding UI panels into and out of visibility
/// </summary>
public class PanelUI : MonoBehaviour {
    #region Variables
    
    private RectTransform _rect;

    [SerializeField]
    private float _slideSpeed = 5;

    [SerializeField]
    private Vector3 _visiblePosition = new Vector3(-200, 0, 0);

    [SerializeField]
    private Vector3 _hiddenPosition = new Vector3(200, 0, 0);

    [SerializeField]
    private bool _visible;

    public bool Visible {
        get { return _visible; }
        set { _visible = value; }
    }
    
    #endregion

    private void Start() {
        _rect = GetComponent<RectTransform>();

        _rect.anchoredPosition = _visible ? _visiblePosition : _hiddenPosition;
    }

    private void Update() {
        if (_visible) {
            _rect.anchoredPosition = Vector3.Lerp(_rect.anchoredPosition, _visiblePosition,
                Time.unscaledDeltaTime * _slideSpeed);
        }
        else {
            _rect.anchoredPosition =
                Vector3.Lerp(_rect.anchoredPosition, _hiddenPosition, Time.unscaledDeltaTime * _slideSpeed);
        }
    }

    /// <summary>
    /// Set visible for an amount of time
    /// </summary>
    /// <param name="seconds">The amount of seconds the panel should be visible for</param>
    public void SetVisibilityForSeconds(float seconds) {
        StartCoroutine(VisibleForSeconds(seconds));
    }

    private IEnumerator VisibleForSeconds(float seconds) {
        _visible = true;

        yield return new WaitForSeconds(seconds);

        _visible = false;
    }

    /// <summary>
    /// Set the visible and hidden positions
    /// </summary>
    /// <param name="visible">The positions where the panel should be when visible</param>
    /// <param name="hidden">The positions where the panel should be when hidden</param>
    public void SetVisibilityPositions(Vector3 visible, Vector3 hidden) {
        _visiblePosition = visible;
        _hiddenPosition = hidden;
    }
}