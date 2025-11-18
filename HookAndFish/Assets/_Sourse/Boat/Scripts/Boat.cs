using UnityEngine;
using System.Collections.Generic;

public class Boat : MonoBehaviour
{
    [SerializeField] private LevelFinisher _levelFinisher;
    [SerializeField] private BoatMover _boatMover;
    [SerializeField] private ParticleSystem _motorSplashFx;
    
    private readonly List<AreaForBoat> _allTargetAreas = new List<AreaForBoat>();
    private float _arriveDistance = 0.25f; 
    private bool _isInitialized;

    private void OnEnable()
    {
        if (_isInitialized)
            UpdateNearestTarget();
    }

    private void OnDisable()
    {
        StopMotorFx(true);
    }

    private void Update()
    {
        if (_isInitialized)
            UpdateNearestTarget();
    }
    
    public void Initialize(IEnumerable<AreaForBoat> targetAreas)
    {
        _allTargetAreas.Clear();

        if (targetAreas != null)
            _allTargetAreas.AddRange(targetAreas);

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
        
        if (nearestDistance <= _arriveDistance)
        {
            StopMotorFx();
            
            return;
        }
        
        _boatMover.SetTarget(nearestArea.transform.position);
        
        if (_motorSplashFx && !_motorSplashFx.isPlaying)
            _motorSplashFx.Play();
    }

    private void StopMotorFx(bool clear = false)
    {
        if (_motorSplashFx && _motorSplashFx.isPlaying)
            _motorSplashFx.Stop(true, clear ? ParticleSystemStopBehavior.StopEmittingAndClear
                                            : ParticleSystemStopBehavior.StopEmitting);
    }
}
