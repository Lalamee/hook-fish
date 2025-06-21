using UnityEngine;

public class BadEnd : MonoBehaviour
{
    public void TurnOnObject()
    {
        gameObject.SetActive(true);
    }

    public void TurnOffObject()
    {
        gameObject.SetActive(false);
    }
}
