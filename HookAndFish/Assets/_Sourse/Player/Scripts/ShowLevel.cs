using UnityEngine;

public class ShowLevel : MonoBehaviour
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
