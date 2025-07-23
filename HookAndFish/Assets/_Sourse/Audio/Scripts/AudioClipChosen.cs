using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class AudioClipChosen : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip goodEndClip;
    [SerializeField] private AudioClip badEndClip;

    public void PlayGoodAudio()
    {
        _audioSource.clip = goodEndClip;
        _audioSource.Play();
    }

    public void PlayBadAudio()
    {
        _audioSource.clip = badEndClip;
        _audioSource.Play();
    }
    
    public void PlayGoodAudioInPlace()
    {
        Camera mainCamera = Camera.main;
        Vector3 playPosition = mainCamera.transform.position;
        AudioSource.PlayClipAtPoint(goodEndClip, playPosition);
    }
}
