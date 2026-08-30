using UnityEngine;

[RequireComponent(typeof(BoatMover))]
public class StateSwitcherPlayer : MonoBehaviour
{
    [SerializeField] private BoatMover _boatMover;
    [SerializeField] private HarpoonControl _harpoonControl;
    [SerializeField] private Laser _laser;
    [SerializeField] private Hook _hook;
    
    private Vector3 _targetPosition;
    private float rotationSpeed = 5f;
    private float moveSpeed = 5f; 
    private bool _isProcessing = false;
    
    private void Update()
    {
        if (_isProcessing)
        {
            RotateAndMoveToCenter();
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.TryGetComponent(out AreaForBoat areaForBoat))
        {
            StartProcessing(areaForBoat.transform.position);
            areaForBoat.StartSpawnFish();
        }
    }

    private void StartProcessing(Vector3 centerPosition)
    {
        _isProcessing = true;
        _targetPosition = centerPosition;
        _boatMover.enabled = false;
    }

    private void RotateAndMoveToCenter()
    {
        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position, _targetPosition, moveSpeed * Time.deltaTime);

        if (Quaternion.Angle(transform.rotation, targetRotation) < 1.1f)
        {
            _isProcessing = false;
            _hook.enabled = true;
            _laser.OnRenderer();
            _harpoonControl.enabled = true;
        }
    }
}