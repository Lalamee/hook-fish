using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Rope : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _hook;
    [SerializeField] private Transform _harpoon;
    [SerializeField] private Hook _hookScript; 

    [Header("Appearance Settings")]
    [SerializeField] private int _segmentCount = 25;
    [SerializeField] private float _baseAmplitude = 0.1f;
    [SerializeField] private float _waveFrequency = 10f;
    [SerializeField] private float _waveSpeed = 4f;
    [SerializeField] private float _lineWidth = 0.06f;
    [SerializeField] private Color _lineColor = new Color(0.1f, 0.1f, 0.1f, 1f);

    private LineRenderer _lineRenderer;
    private Material _lineMaterial;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        
        _lineRenderer.useWorldSpace = true;
        _lineRenderer.positionCount = _segmentCount;
        _lineMaterial = new Material(Shader.Find("Sprites/Default"));
        _lineRenderer.material = _lineMaterial;
        _lineRenderer.startColor = _lineColor;
        _lineRenderer.endColor = _lineColor;
        _lineRenderer.startWidth = _lineWidth;
        _lineRenderer.endWidth = _lineWidth;
        _lineRenderer.numCapVertices = 2;
    }

    private void Update()
    {
        if (_hook == null || _harpoon == null || _hookScript == null)
        {
            _lineRenderer.enabled = false;
            return;
        }
        
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
        Vector3 start = _harpoon.position;
        Vector3 end = _hook.position;
        float distance = Vector3.Distance(start, end);
        float dynamicAmplitude = _baseAmplitude + distance * 0.05f; 

        for (int i = 0; i < _segmentCount; i++)
        {
            float t = i / (float)(_segmentCount - 1);
            Vector3 point = Vector3.Lerp(start, end, t);
            
            float wave = Mathf.Sin(t * _waveFrequency + Time.time * _waveSpeed) * dynamicAmplitude;
            Vector3 offset = Vector3.up * wave;

            _lineRenderer.SetPosition(i, point + offset);
        }
    }
}
