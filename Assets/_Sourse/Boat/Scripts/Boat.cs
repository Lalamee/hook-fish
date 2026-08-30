using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{
    private const float ArriveDistance = 0.25f;

    [SerializeField] private LevelFinisher _levelFinisher;
    [SerializeField] private BoatMover _boatMover;
    [SerializeField] private ParticleSystem _motorSplashFx;

    private readonly List<AreaForBoat> _allTargetAreas = new List<AreaForBoat>();
    private bool _isInitialized;

    private void OnEnable()
    {
        if (_isInitialized)
        {
            UpdateNearestTarget();
        }
    }

    private void OnDisable()
    {
        StopMotorFx(true);
    }

    private void Update()
    {
        if (_isInitialized)
        {
            UpdateNearestTarget();
        }
    }
    
    public void Initialize(IEnumerable<AreaForBoat> targetAreas)
    {
        _allTargetAreas.Clear();

        if (targetAreas != null)
        {
            _allTargetAreas.AddRange(targetAreas);
        }

        _isInitialized = true;
    }

    private void UpdateNearestTarget()
    {
        if (_boatMover == null || !_boatMover.enabled)
        {
            StopMotorFx();
            return;
        }

        _allTargetAreas.RemoveAll(area => area == null);

        if (_allTargetAreas.Count == 0)
        {
            StopMotorFx();
            _levelFinisher?.GoodEnd();
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

        if (nearestArea == null)
        {
            StopMotorFx();
            return;
        }

        if (nearestDistance <= ArriveDistance)
        {
            StopMotorFx();
            return;
        }

        _boatMover.SetTarget(nearestArea.transform.position);

        if (_motorSplashFx != null && !_motorSplashFx.isPlaying)
        {
            _motorSplashFx.Play();
        }
    }

    private void StopMotorFx(bool clear = false)
    {
        if (_motorSplashFx != null && _motorSplashFx.isPlaying)
        {
            ParticleSystemStopBehavior stopBehavior = clear
                ? ParticleSystemStopBehavior.StopEmittingAndClear
                : ParticleSystemStopBehavior.StopEmitting;
            _motorSplashFx.Stop(true, stopBehavior);
        }
    }
}
