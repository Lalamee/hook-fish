using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Camera mainCamera;
    
    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (mainCamera != null)
        {
            transform.LookAt(2 * transform.position - mainCamera.transform.position);
        }
    }
}