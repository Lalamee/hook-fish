using System.Collections;
using UnityEngine;

[RequireComponent(typeof(FixedJoint), typeof(SpringJoint), typeof(Rigidbody))]
public class Hook : MonoBehaviour
{
    [SerializeField] private float _returnTime = 0.5f;
    [SerializeField] private Transform _harpoonTransform;
    [SerializeField] private HarpoonControl _harpoonControl;
    [SerializeField] private Laser _laser;
    [SerializeField] private FixedJoint _fixed;
    [SerializeField] private SpringJoint _spring;
    
    private Vector3 _initialPosition;
    private Vector3 _targetPosition;
    private bool _isMoving;
    private bool _isReturning;
    private float _returnTimer;
    private float _speed = 20f;

    public bool IsHookActive => _isMoving || _isReturning;

    private void Start()
    {
        _spring.connectedBody = _harpoonTransform.GetComponent<Rigidbody>();
        _spring.spring = 100f;
        _spring.damper = 5f;
        _spring.minDistance = 0f;
        _spring.maxDistance = 3f;
        _spring.enableCollision = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0) && !_isMoving && !_isReturning)
        {
            _initialPosition = transform.position;
            _isMoving = true;

            _laser.OffRenderer();
            _harpoonControl.LockMovement();

            StartCoroutine(ReturnTimerCoroutine());
        }

        if (_isMoving)
        {
            transform.Translate(Vector3.forward * _speed * Time.deltaTime);
        }

        if (_isReturning)
        {
            _returnTimer += Time.deltaTime;
            float lerpProgress = _returnTimer / _returnTime;
            transform.position = Vector3.Lerp(_targetPosition, _initialPosition, lerpProgress);

            if (lerpProgress >= 1f)
            {
                _fixed.connectedBody = null;
                _harpoonControl.UnlockMovement();
                _laser.OnRenderer();
                _isReturning = false;
                _returnTimer = 0f;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!_isReturning && collision.gameObject.TryGetComponent(out TrappedFish trappedFish))
        {
            _targetPosition = transform.position;
            _isMoving = false;
            _isReturning = true;

            trappedFish.GetComponent<Collider>().enabled = false;
            _fixed.connectedBody = trappedFish.GetComponent<Rigidbody>();
            trappedFish.CheckPosition(_initialPosition, _returnTime, _returnTimer);
        }
    }

    private IEnumerator ReturnTimerCoroutine()
    {
        yield return new WaitForSeconds(_returnTime);
        if (_isMoving && !_isReturning)
        {
            _targetPosition = transform.position;
            _isMoving = false;
            _isReturning = true;
        }
    }
}
