using UnityEngine;
using System.Collections.Generic;

public class Boat : MonoBehaviour
{
    [SerializeField] private LevelFinisher _levelFinisher;
    [SerializeField] private BoatMover _boatMover;

    private List<AreaForBoat> _allTargetAreas;

    private void Awake()
    {
        _allTargetAreas = new List<AreaForBoat>();
    }

    private void OnEnable()
    {
        FindAllTargetAreas();
        UpdateNearestTarget();
    }

    private void Update()
    {
        UpdateNearestTarget();
    }

    private void FindAllTargetAreas()
    {
        _allTargetAreas.Clear();
        AreaForBoat[] areas = FindObjectsOfType<AreaForBoat>();
        _allTargetAreas.AddRange(areas);
    }

    private void UpdateNearestTarget()
    {
        _allTargetAreas.RemoveAll(area => area == null);

        if (_allTargetAreas.Count == 0)
        {
            _levelFinisher.GoodEnd();
            return;
        }

        AreaForBoat nearestArea = null;
        float nearestDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (AreaForBoat area in _allTargetAreas)
        {
            float distance = Vector3.Distance(currentPosition, area.transform.position);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestArea = area;
            }
        }

        if (nearestArea != null)
        {
            _boatMover.SetTarget(nearestArea.transform.position);
        }
    }
}