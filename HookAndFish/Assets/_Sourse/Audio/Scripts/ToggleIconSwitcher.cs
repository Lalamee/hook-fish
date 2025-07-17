using UnityEngine;
using UnityEngine.UI;

public class ToggleIconSwitcher : MonoBehaviour
{
    [SerializeField] private Toggle _toggle;
    [SerializeField] private Image _iconImage;
    [SerializeField] private Sprite _spriteOn;   
    [SerializeField] private Sprite _spriteOff;  

    private void Start()
    {
        UpdateIcon(_toggle.isOn);
        _toggle.onValueChanged.AddListener(UpdateIcon);
    }

    private void UpdateIcon(bool isOn)
    {
        _iconImage.sprite = isOn ? _spriteOn : _spriteOff;
    }
}
