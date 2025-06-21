using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Laser : MonoBehaviour
{
    [SerializeField] private LineRenderer _laser;
    [SerializeField] Material _laserMaterial;
    
    private bool _isRenderer;
    private float _lineLength = 15f;

    private void Start()
    {
        _laser.positionCount = 2;
        _laser.material = _laserMaterial;
        
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[]
            {
                new GradientColorKey(Color.red, 0f),
                new GradientColorKey(Color.clear, 1f)
            },
            new GradientAlphaKey[]
            {
                new GradientAlphaKey(1f, 0f),
                new GradientAlphaKey(0f, 1f)
            }
        );
        _laser.colorGradient = gradient;

        _laser.startWidth = 0.06f;
        _laser.endWidth = 0.06f;
        _laser.enabled = false;
    }

    private void Update()
    {
        if (_isRenderer)
        {
            Vector3 start = transform.position;
            Vector3 end = start + transform.forward * _lineLength;
            _laser.SetPosition(0, start);
            _laser.SetPosition(1, end);
        }
    }

    public void OnRenderer()
    {
        _laser.enabled = true;
        _isRenderer = true;
    }

    public void OffRenderer()
    {
        _laser.enabled = false;
        _isRenderer = false;
    }
}