using UnityEngine;

public class BlinkAnimation : MonoBehaviour
{
    [SerializeField] private float _blinkDuration = 1.0f;

    private Vector3 _visibleSize;
    private float _currentBlinkTime;
    private bool _isVisible;

    private void Start()
    {
        _visibleSize = transform.localScale;
        _currentBlinkTime = _blinkDuration;
    }

    private void Update()
    {
        _currentBlinkTime -= Time.deltaTime;
        if (_currentBlinkTime < 0.0f)
        {
            if (_isVisible)
                transform.localScale = Vector3.zero;
            else transform.localScale = _visibleSize;

            _isVisible = !_isVisible;
            _currentBlinkTime = _blinkDuration + _currentBlinkTime;
        }
    }
}
