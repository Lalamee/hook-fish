using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset;

    private float _smoothSpeed = 5f;
    private float _distance = 5f; 
    private float _height = 15f; 
    private bool _isSwims = true;

    private void Update()
    {
        PlayerSwims();
    }

    public void ChangeState() 
    { 
        _isSwims = !_isSwims; 
    }

    private void PlayerSwims()
    {
        Vector3 desiredPosition = _target.position + _offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed);
        transform.position = smoothedPosition;
        transform.LookAt(_target);
    }

    private void PlayerCatch()
    {
        Vector3 targetPosition = _target.position - _target.forward * _distance + Vector3.up * _height;
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * _smoothSpeed);
        Vector3 lookAtPosition = _target.position;
        lookAtPosition.y = _height; 
        transform.rotation = Quaternion.LookRotation(-lookAtPosition);
    }
}
