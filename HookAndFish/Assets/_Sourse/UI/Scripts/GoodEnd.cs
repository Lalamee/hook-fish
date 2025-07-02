using UnityEngine;

public class GoodEnd : MonoBehaviour
{
    public void TurnOnObject()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void TurnOffObject()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
