using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

[RequireComponent(typeof(BoatMover))]
public class StateSwitcherPlayer : MonoBehaviour
{
    private HarpoonControl _harpoonControl;
    private BoatMover _boatMover;
    private Laser _laser;
    private Hook _hook;
    private Vector3 _targetPosition;
    private float rotationSpeed = 5f;
    private float moveSpeed = 5f; 
    private bool _isProcessing = false;

    private void Start()
    {
        _boatMover = GetComponent<BoatMover>();

        _harpoonControl = FindObjectOfType<HarpoonControl>();
        _hook = FindObjectOfType<Hook>();
        _laser = FindObjectOfType<Laser>();
    }

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