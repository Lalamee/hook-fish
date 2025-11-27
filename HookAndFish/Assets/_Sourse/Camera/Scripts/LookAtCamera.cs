using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Camera _mainCamera;
    private float _reflectionFactor; 
    
    private void Start()
    {
        _mainCamera = Camera.main;
        _reflectionFactor = 2f;
    }

    private void Update()
    {
        if (_mainCamera != null)
        {
            transform.LookAt(_reflectionFactor * transform.position - _mainCamera.transform.position);
        }
    }
}