using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Transform _transform;
    private Camera mainCamera;
    
    private void Start()
    {
        _transform = GetComponent<Transform>();
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (mainCamera != null)
        {
            _transform.LookAt(2 * _transform.position - mainCamera.transform.position);
        }
    }
}