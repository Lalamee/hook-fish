using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FishMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;

    private Boat _boat;
    private int _direction;
    private float _rotationY;
    private float _zero;
    private bool _movementAllowed;

    private void OnEnable()
    {
        _boat = FindObjectOfType<Boat>();
        _rotationY = -90f;
        _zero = 0f;
        
        float boatX = _boat.transform.position.x;
        _direction = boatX < transform.position.x ? -1 : 1;
        
        transform.localRotation = Quaternion.Euler(0f, _rotationY * _direction, 0f);
    }

    private void Update()
    {
        if (!_movementAllowed) 
            return;
        
        Move();
    }

    private void Move()
    {
        float scaledMoveSpeed = _moveSpeed * Time.deltaTime;
        Vector3 moveDirection = new Vector3(scaledMoveSpeed * _direction, _zero, _zero);
        transform.position += moveDirection;
    }

    public void SetMovementAllowed(bool allowed)
    {
        _movementAllowed = allowed;
    }
}