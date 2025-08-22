using UnityEngine;
using UnityEngine.EventSystems;

public class GameUI : MonoBehaviour
{
    [SerializeField] private GameObject menuRoot;

    public static bool IsOpen { get; private set; }

    public void OpenMenu()
    {
        IsOpen = true;
        menuRoot.SetActive(true);
        Time.timeScale = 0f;
    }

    public void CloseMenu() => StartCoroutine(CloseAfterRelease());

    private System.Collections.IEnumerator CloseAfterRelease()
    {
        yield return null;
        
        while (Input.GetMouseButton(0) || Input.touchCount > 0)
            yield return null;
        
        Time.timeScale = 1f;
        menuRoot.SetActive(false);
        IsOpen = false;
    }
}