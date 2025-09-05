using System.Collections;
using UnityEngine;

[RequireComponent(typeof(FixedJoint), typeof(SpringJoint), typeof(Rigidbody))]
public class Hook : MonoBehaviour
{
    [SerializeField] private BoatRecoil _recoil;
    [SerializeField] private float _returnTime = 0.55f;
    [SerializeField] private Transform _harpoonTransform;
    [SerializeField] private HarpoonControl _harpoonControl;
    [SerializeField] private Laser _laser;
    [SerializeField] private FixedJoint _fixed;
    [SerializeField] private SpringJoint _spring;

    private enum HookState { Idle, Flying, Returning }
    private HookState _state = HookState.Idle;

    private Vector3 _initialPosition;
    private Vector3 _targetPosition;
    private float _returnTimer;
    private float _speed = 25f;

    public bool IsHookActive => _state != HookState.Idle;

    private void Start()
    {
        _spring.connectedBody = _harpoonTransform.GetComponent<Rigidbody>();
        _spring.spring = 100f;
        _spring.damper = 5f;
        _spring.minDistance = 0f;
        _spring.maxDistance = 3f;
        _spring.enableCollision = false;

        // В исходном состоянии лазер включён
        _laser.OnRenderer();
    }

    private void Update()
    {
        if (GameUI.IsOpen)
            return;

        if (Input.GetMouseButtonUp(0) && _state == HookState.Idle)
        {
            Fire();
        }

        if (_state == HookState.Flying)
        {
            transform.Translate(Vector3.forward * _speed * Time.deltaTime);
        }
        else if (_state == HookState.Returning)
        {
            Return();
        }
    }

    private void Fire()
    {
        _recoil.DoRecoil();

        _initialPosition = transform.position;
        _state = HookState.Flying;
        
        _laser.OffRenderer();

        _harpoonControl.LockMovement();
        
        StartCoroutine(DelayedAutoReturn());
    }

    private void Return()
    {
        _returnTimer += Time.deltaTime;
        float t = Mathf.Clamp01(_returnTimer / _returnTime);
        transform.position = Vector3.Lerp(_targetPosition, _initialPosition, t);

        if (t >= 1f)
        {
            CompleteReturn();
        }
    }
    
    private void BeginReturn()
    {
        if (_state == HookState.Returning) return;

        _targetPosition = transform.position; 
        _returnTimer = 0f;                    
        _state = HookState.Returning;
    }

    private void CompleteReturn()
    {
        _fixed.connectedBody = null;
        _harpoonControl.UnlockMovement();

        _state = HookState.Idle;
        _returnTimer = 0f;
        
        _laser.OnRenderer();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_state == HookState.Flying && collision.gameObject.TryGetComponent(out TrappedFish trappedFish))
        {
            BeginReturn();

            trappedFish.GetComponent<Collider>().enabled = false;
            _fixed.connectedBody = trappedFish.GetComponent<Rigidbody>();
            trappedFish.CheckPosition(_initialPosition, _returnTime, _returnTimer);
        }
    }

    private IEnumerator DelayedAutoReturn()
    {
        yield return new WaitForSeconds(_returnTime);

        // Если не попали — инициируем возврат
        if (_state == HookState.Flying)
        {
            BeginReturn();
        }
    }
}
