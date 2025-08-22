using UnityEngine;
using System.Collections.Generic;

public class Boat : MonoBehaviour
{
    [SerializeField] private LevelFinisher _levelFinisher;
    [SerializeField] private BoatMover _boatMover;
    [SerializeField] private ParticleSystem _motorSplashFx;
    
    private float _arriveDistance = 0.25f; 
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

    private void OnDisable()
    {
        StopMotorFx(true);
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
        if (_boatMover == null || !_boatMover.enabled)
        {
            StopMotorFx();
            
            return;
        }

        _allTargetAreas.RemoveAll(area => area == null);

        if (_allTargetAreas.Count == 0)
        {
            StopMotorFx();
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

    public void StopMotorFx(bool clear = false)
    {
        if (_motorSplashFx && _motorSplashFx.isPlaying)
            _motorSplashFx.Stop(true, clear ? ParticleSystemStopBehavior.StopEmittingAndClear
                                            : ParticleSystemStopBehavior.StopEmitting);
    }
}
