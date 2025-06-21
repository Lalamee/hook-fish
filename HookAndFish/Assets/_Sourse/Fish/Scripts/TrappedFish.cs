using UnityEngine;

[RequireComponent(typeof(FishMover))]
[RequireComponent(typeof(FishLevelTransmitter))]
public class TrappedFish : MonoBehaviour
{

    [SerializeField] private FishLevelTransmitter _fishLevelTransmitter;
    [SerializeField] private FishMover _fishMover;
    private Vector3 _targetPosition;
    private Vector3 _finishPosition;
    private bool _isLevelChange;
    private bool _isMoving;
    private float _returnTimer;
    private float _returnTime;
    private int _lerpLimit;

    private void Start()
    {
        _isLevelChange = false;
        _isMoving = false;
        _lerpLimit = 1;
    }


    private void Update()
    {
        if (_isMoving)
        {
            _returnTimer += Time.deltaTime;
            float lerpProgress = _returnTimer / _returnTime;

            if (lerpProgress >= _lerpLimit && !_isLevelChange)
            {
                _fishLevelTransmitter.TransmitAndDestroy();
                _isLevelChange = true;
            }
        }
    }

    public void CheckPosition(Vector3 finishPosition, float returnTime, float returnTimer)
    {
        _fishMover.enabled = false;
        _isMoving = true;
        _targetPosition = transform.position;
        _finishPosition = finishPosition;
        _returnTime = returnTime;
        _returnTimer = returnTimer;
    }
}
