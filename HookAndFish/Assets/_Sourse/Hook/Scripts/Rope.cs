using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Rope : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _hook;
    [SerializeField] private Transform _harpoon;
    [SerializeField] private Hook _hookScript;
    [SerializeField] private LineRenderer _lineRenderer;

    [Header("Appearance Settings")]
    [SerializeField] private Material _lineMaterial;
    [SerializeField] private float _lineWidth = 0.055f;
    [SerializeField] private Color _lineColor = new Color(0.08f, 0.08f, 0.08f, 1f);

    private void Awake()
    {
        _lineRenderer.useWorldSpace = true;
        _lineRenderer.material = _lineMaterial;
        _lineRenderer.startColor = _lineColor;
        _lineRenderer.endColor = _lineColor;
        _lineRenderer.startWidth = _lineWidth;
        _lineRenderer.endWidth = _lineWidth;
        _lineRenderer.alignment = LineAlignment.View;
        _lineRenderer.positionCount = 2;
    }

    private void Update()
    {
        if (!_hook || !_harpoon || !_hookScript) { _lineRenderer.enabled = false; return; }

        if (_hookScript.IsHookActive)
        {
            _lineRenderer.enabled = true;
            DrawWavyRope(); 
        }
        else
        {
            _lineRenderer.enabled = false;
        }
    }

    private void DrawWavyRope()
    {
        _lineRenderer.SetPosition(0, _harpoon.position);
        _lineRenderer.SetPosition(1, _hook.position);
    }
}