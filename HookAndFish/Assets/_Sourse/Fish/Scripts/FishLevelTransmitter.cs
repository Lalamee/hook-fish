using UnityEngine;

[RequireComponent(typeof(Fish))]
[RequireComponent(typeof(AudioClipChosen ))]
public class FishLevelTransmitter : MonoBehaviour
{
    [SerializeField] private Fish _fish;
    [SerializeField] private AudioClipChosen _audioClipChosen;
    
    private Player _player;
    private LevelFinisher _levelFinisher;

    private void Start()
    {
        _player = FindObjectOfType<Player>();
        _levelFinisher = FindObjectOfType<LevelFinisher>();
    }

    public void TransmitAndDestroy()
    {
        if (_player.IsPlayerLevelMore(_fish.Level))
        {
            _fish.PlayCatchFX();
            _audioClipChosen.PlayGoodAudioInPlace();
            _player.CatchFish(_fish.Level);
            Destroy(_fish.gameObject);
        }
        else
        {
            _audioClipChosen.PlayBadAudio();
            _levelFinisher.BadEnd();
        }
    }
}
