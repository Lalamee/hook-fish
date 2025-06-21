using UnityEngine;

public class DestroyZone : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Fish fish))
        {
            Destroy(collision.gameObject);
        }
    }
}
