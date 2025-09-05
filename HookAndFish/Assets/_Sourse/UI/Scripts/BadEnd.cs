using UnityEngine;

public class BadEnd : MonoBehaviour
{
    [SerializeField] private Popup popup;   

    public void TurnOnObject()
    {
        gameObject.SetActive(true);
        popup?.PlayShow();
    }

    public void TurnOffObject()
    {
        if (popup == null)
        {
            gameObject.SetActive(false);
            return;
        }

        popup.PlayHide(() =>
        {
            gameObject.SetActive(false);
        });
    }
}