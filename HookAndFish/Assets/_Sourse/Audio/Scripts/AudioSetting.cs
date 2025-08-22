using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using YG;

public class AudioSetting : MonoBehaviour, IPointerUpHandler, IEndDragHandler
{
    [SerializeField] private AudioMixerGroup _mixer;
    [SerializeField] private string _parameter = "MasterVolume";
    [SerializeField] private Slider _volumeSlider;
    [SerializeField] private Toggle _muteToggle;
    [SerializeField] private Image _iconImage;
    [SerializeField] private Sprite _spriteOn;
    [SerializeField] private Sprite _spriteOff;
    [SerializeField] private AudioSource _audioSource;

    private const float MinVolumeThreshold = 0.01f;
    private bool _internalToggleChange = false;
    private float _lastPlayTime = -1f;

    public void OnPointerUp(PointerEventData eventData) => PlayReleaseSound();
    public void OnEndDrag(PointerEventData eventData)   => PlayReleaseSound();

    private void OnEnable()
    {
        SyncUIFromSaves();
    }

    private void Start()
    {
        if (_volumeSlider != null)
            _volumeSlider.onValueChanged.AddListener(OnVolumeChanged);

        if (_muteToggle != null)
            _muteToggle.onValueChanged.AddListener(OnMuteToggled);

        SyncUIFromSaves();
    }

    private void SyncUIFromSaves()
    {
        float saved = Mathf.Clamp01(YG2.saves.volume);
        bool isMuted = saved <= MinVolumeThreshold;

        if (_volumeSlider != null)
            _volumeSlider.SetValueWithoutNotify(saved);

        if (_muteToggle != null)
        {
            _internalToggleChange = true;
            _muteToggle.isOn = isMuted;
            _internalToggleChange = false;
        }

        ApplyVolume(saved, isMuted);
        UpdateIcon(saved);
    }

    private void OnVolumeChanged(float value)
    {
        bool isMuted = _muteToggle != null && _muteToggle.isOn;
        
        ApplyVolume(value, isMuted);
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
            float restoreVolume = Mathf.Max(YG2.saves.volume, 0.5f);
            ApplyVolume(restoreVolume, false);
            
            if (_volumeSlider != null) 
                _volumeSlider.SetValueWithoutNotify(restoreVolume);
            
            UpdateIcon(restoreVolume);
        }
    }

    private void ApplyVolume(float value, bool isMuted)
    {
        float volumeDb = isMuted ? -80f : Mathf.Log10(Mathf.Max(value, 0.0001f)) * 20f;
        
        if (_mixer != null) 
            _mixer.audioMixer.SetFloat(_parameter, volumeDb);
        
        YG2.saves.volume = isMuted ? 0f : Mathf.Clamp01(value);
        YG2.SaveProgress();
    }

    private void UpdateIcon(float volumeValue)
    {
        if (_iconImage == null || _spriteOn == null || _spriteOff == null) 
            return;
        
        _iconImage.sprite = (volumeValue <= MinVolumeThreshold) ? _spriteOff : _spriteOn;
    }

    private void PlayReleaseSound()
    {
        if (_audioSource == null)
            return;
        
        if (Time.unscaledTime - _lastPlayTime < 0.05f)
            return;
        
        if (!_audioSource.isPlaying) 
            _audioSource.Play();
        
        _lastPlayTime = Time.unscaledTime;
    }
}
