using UnityEngine;

public class BadEnd : MonoBehaviour
{
    [SerializeField] private UIInGame _uiInGame;
    
    public void TurnOnObject()
    {
        gameObject.SetActive(true);
        _uiInGame.gameObject.SetActive(false);
    }

    public void TurnOffObject()
    {
        gameObject.SetActive(false);

    }
}