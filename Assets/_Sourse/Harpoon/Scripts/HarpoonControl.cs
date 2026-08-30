using UnityEngine;

public class HarpoonControl : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;
    
    private HarpoonInput _input;
    private bool _isMovementLocked;

    private void Awake()
    {
        _input = new HarpoonInput();
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    private void Start()
    {
        _isMovementLocked = false;
    }

    private void Update()
    {
        if(Input.GetMouseButton(0) && !_isMovementLocked)
            RotateObject();
    }
    
    public void LockMovement()
    {
        _isMovementLocked = true;
    }

    public void UnlockMovement()
    {
        _isMovementLocked = false;
    }

    private void RotateObject()
    {
        Vector2 mouseDelta = _input.Harpoon.Rotation.ReadValue<Vector2>();
        float rotationY = mouseDelta.x * _rotationSpeed * Time.deltaTime;
        
        Vector3 currentRotation = transform.rotation.eulerAngles;
        float newRotationY = currentRotation.y + rotationY;

        float controlRotation = 180f;
        float newRotation = 360f;
        
        if (newRotationY > controlRotation)
            newRotationY -= newRotation;
        else if (newRotationY < -controlRotation)
            newRotationY += newRotation;

        float limitationClamp = 60f;

        float clampedRotationY = Mathf.Clamp(newRotationY, -limitationClamp, limitationClamp);
        transform.rotation = Quaternion.Euler(currentRotation.x, clampedRotationY, currentRotation.z);
    }
}
