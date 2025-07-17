using UnityEngine;
using UnityEngine.UI;

public class SliderIconSwitcher : MonoBehaviour
{
    [SerializeField] private Slider _volumeSlider;
    [SerializeField] private Image _iconImage;
    [SerializeField] private Sprite _spriteOn;
    [SerializeField] private Sprite _spriteOff;

    private const float _minVolumeThreshold = 0.01f;

    private void Start()
    {
        _volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        UpdateIcon(_volumeSlider.value);
    }

    private void OnVolumeChanged(float value)
    {
        UpdateIcon(value);
    }

    private void UpdateIcon(float volumeValue)
    {
        if (_iconImage == null || _spriteOn == null || _spriteOff == null) return;
        _iconImage.sprite = (volumeValue <= _minVolumeThreshold) ? _spriteOff : _spriteOn;
    }
}
