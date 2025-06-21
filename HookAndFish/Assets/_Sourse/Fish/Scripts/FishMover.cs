using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FishMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5;
    
    private Boat _boat;
    private Vector3 _initialOffset;
    private int _direction;
    private int _directionNumber;
    private float _rotationY;
    private float _zero;

    private void Start()
    {
        _boat = FindObjectOfType<Boat>();
        _directionNumber = 1;
        _rotationY = -90f;
        _zero = 0f;

        float boatX = _boat.transform.position.x;

        if (boatX < transform.position.x)
            _direction = -_directionNumber;
        else
            _direction = _directionNumber;
        
        gameObject.transform.Rotate(_zero,_rotationY * _direction,_zero);
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float scaledMoveSpeed = _moveSpeed * Time.deltaTime;
        Vector3 moveDirection = new Vector3(scaledMoveSpeed * _direction, _zero, _zero);
        
        transform.position += moveDirection;
    }
}
