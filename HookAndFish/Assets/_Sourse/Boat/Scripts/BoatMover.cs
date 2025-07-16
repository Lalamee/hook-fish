using System;
using UnityEngine;

public class BoatMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 10f;
    [SerializeField] private float _rotationSpeed = 4f;
    [SerializeField] private ParticleSystem _particleSystem;

    private Vector3 _currentTarget;
    private bool _hasTarget = false;

    private void Update()
    {
        if (_hasTarget)
        {
            MoveToTarget(_currentTarget);
        }
    }

    private void OnEnable()
    {
        if (!_particleSystem.isPlaying)
            _particleSystem.Play();
    }

    private void OnDisable()
    {
        if (_particleSystem.isPlaying)
            _particleSystem.Stop();
    }

    public void SetTarget(Vector3 target)
    {
        _currentTarget = target;
        _hasTarget = true;
    }

    private void MoveToTarget(Vector3 target)
    {
        float scaledMoveSpeed = _moveSpeed * Time.deltaTime;

        Vector3 moveDirection = (target - transform.position).normalized;
        transform.position += moveDirection * scaledMoveSpeed;

        RotateToTarget(moveDirection);
    }

    private void RotateToTarget(Vector3 moveDirection)
    {
        float scaledRotationSpeed = _rotationSpeed * Time.deltaTime;

        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, scaledRotationSpeed);
        }
    }
}