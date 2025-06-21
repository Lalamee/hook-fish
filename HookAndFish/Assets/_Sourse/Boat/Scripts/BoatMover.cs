using UnityEngine;
using System.Collections.Generic;

public class BoatMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 10f;
    [SerializeField] private float _rotationSpeed = 4f;

    private LevelFinisher _levelFinisher;
    private List<AreaForBoat> _allTargetAreas = new List<AreaForBoat>();

    private void Start()
    {
        _levelFinisher = FindObjectOfType<LevelFinisher>();
    }

    private void Update()
    {
        Vector3 nearestTarget = FindNearestTarget();
        MoveToTarget(nearestTarget);
    }

    private void OnEnable()
    {
        FindAllTargetAreas();
    }

    private void FindAllTargetAreas()
    {
        _allTargetAreas.Clear();
        AreaForBoat[] areas = FindObjectsOfType<AreaForBoat>();
        _allTargetAreas.AddRange(areas);
    }

    private Vector3 FindNearestTarget()
    {
        _allTargetAreas.RemoveAll(area => area == null);

        if (_allTargetAreas.Count == 0)
        {
            _levelFinisher.GoodEnd();
            return Vector3.zero;
        }

        Vector3 nearestTarget = _allTargetAreas[0].transform.position;
        float nearestDistance = Mathf.Infinity;

        foreach (AreaForBoat area in _allTargetAreas)
        {
            Vector3 targetPoint = area.transform.position;
            float distance = Vector3.Distance(transform.position, targetPoint);

            if (distance < nearestDistance)
            {
                nearestTarget = targetPoint;
                nearestDistance = distance;
            }
        }

        return nearestTarget;
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

        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, scaledRotationSpeed);
    }
}