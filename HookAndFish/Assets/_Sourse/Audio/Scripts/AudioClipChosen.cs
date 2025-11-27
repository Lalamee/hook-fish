using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioClipChosen : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _goodEndClip;
    [SerializeField] private AudioClip _badEndClip;

    private void Awake()
    {
        if (_audioSource == null)
            _audioSource = GetComponent<AudioSource>();
        
        _audioSource.spatialBlend = 0f;
    }

    public void PlayGoodAudio()
    {
        _audioSource.clip = _goodEndClip;
        _audioSource.Play();
    }

    public void PlayBadAudio()
    {
        _audioSource.clip = _badEndClip;
        _audioSource.Play();
    }
}