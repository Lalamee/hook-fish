using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Rope : MonoBehaviour
{
    [SerializeField] private Transform _hookTransform;
    private LineRenderer _lineRenderer;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        DrawRope();
    }

    private void DrawRope()
    {
        if (_hookTransform == null)
        {
            _lineRenderer.positionCount = 0;
            return;
        }

        _lineRenderer.positionCount = 2;
        _lineRenderer.SetPosition(0, transform.position); 
        _lineRenderer.SetPosition(1, _hookTransform.position); 
    }
}

