using UnityEngine;
using System.Collections;

public class GameUI : MonoBehaviour
{
    [SerializeField] private GameObject _menuRoot;
    [SerializeField] private Popup _popup;

    public void OpenMenu()
    {
        _menuRoot.SetActive(true);
        _popup.PlayShow();    
        Time.timeScale = 0f;
    }

    public void CloseMenu() => StartCoroutine(CloseAfterRelease());

    private IEnumerator CloseAfterRelease()
    {
        yield return null;
        while (Input.GetMouseButton(0) || Input.touchCount > 0) yield return null;
      
        _popup.PlayHide(() =>
       {
            Time.timeScale = 1f;
            _menuRoot.SetActive(false);
       });
    }
}