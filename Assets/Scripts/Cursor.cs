using System;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Color _idleColor;
    private Vector3 _idleScale;
    [SerializeField] private Color _holdColor = Color.yellow;
    [SerializeField] private Vector3 _holdScale = new Vector3(.5f, .6f, 1f);

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _idleColor = _spriteRenderer.color;
        _idleScale = transform.localScale;
    }

    void Update()
    {
        // always moving along line
        Vector3 cursorPos = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, -2.25f);
        transform.position = cursorPos;
    }

    public void Hold()
    {
        _spriteRenderer.color = Color.yellow;
        transform.localScale = _holdScale;
    }

    public void Release()
    {
        _spriteRenderer.color = _idleColor;
        transform.localScale = _idleScale;
    }
}
