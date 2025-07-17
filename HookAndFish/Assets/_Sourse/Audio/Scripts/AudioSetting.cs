using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using YG;

public class AudioSetting : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _mixer;
    [SerializeField] private string _parameter = "MasterVolume";

    [SerializeField] private Slider _volumeSlider;
    [SerializeField] private Toggle _muteToggle;

    [Header("Icons for volume state")]
    [SerializeField] private Image _iconImage;
    [SerializeField] private Sprite _spriteOn;  
    [SerializeField] private Sprite _spriteOff;  

    private const float MinVolumeThreshold = 0.01f;
    private bool _internalToggleChange = false;

    private void Start()
    {
        if (_volumeSlider != null)
        {
            _volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
            UpdateIcon(_volumeSlider.value);
        }

        if (_muteToggle != null)
        {
            _muteToggle.onValueChanged.AddListener(OnMuteToggled);
        }
    }

    private void OnVolumeChanged(float value)
    {
        ApplyVolume(value, _muteToggle != null && _muteToggle.isOn);
        UpdateIcon(value);

        if (_muteToggle != null)
        {
            bool shouldMute = value <= MinVolumeThreshold;
            if (_muteToggle.isOn != shouldMute)
            {
                _internalToggleChange = true;
                _muteToggle.isOn = shouldMute;
                _internalToggleChange = false;
            }
        }
    }

    private void OnMuteToggled(bool isMuted)
    {
        if (_internalToggleChange)
            return; 

        if (isMuted)
        {
            ApplyVolume(0f, true);
            
            if (_volumeSlider != null)
                _volumeSlider.SetValueWithoutNotify(0f);
            
            UpdateIcon(0f);
        }
        else
        {
            float restoreVolume = 0.5f;
            ApplyVolume(restoreVolume, false);
            if (_volumeSlider != null)
            {
                _volumeSlider.SetValueWithoutNotify(restoreVolume);
            }
            UpdateIcon(restoreVolume);
        }
    }

    private void ApplyVolume(float value, bool isMuted)
    {
        float volumeDb = isMuted ? -80f : Mathf.Log10(Mathf.Max(value, 0.0001f)) * 20f;
        _mixer.audioMixer.SetFloat(_parameter, volumeDb);
        
        YG2.saves.volume = volumeDb;
    }

    private void UpdateIcon(float volumeValue)
    {
        if (_iconImage == null || _spriteOn == null || _spriteOff == null) return;

        _iconImage.sprite = (volumeValue <= MinVolumeThreshold) ? _spriteOff : _spriteOn;
    }
}
