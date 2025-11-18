using UnityEngine;
using UnityEngine.UI;

public class UIInGame : MonoBehaviour
{
    [SerializeField] private Image[] _interfaceImages;

    public void Init()
    {
        foreach (var image in _interfaceImages)
        {
            image.gameObject.SetActive(true);
        }
    }
}
