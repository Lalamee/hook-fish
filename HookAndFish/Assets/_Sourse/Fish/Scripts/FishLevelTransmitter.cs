using UnityEngine;

[RequireComponent(typeof(Fish))]
[RequireComponent(typeof(AudioClipChosen ))]
public class FishLevelTransmitter : MonoBehaviour
{
    [SerializeField] private Fish _fish;
    [SerializeField] private AudioClipChosen _audioClipChosen;
    
    private Player _player;
    private LevelFinisher _levelFinisher;
    
    public void Initialize(Player player, LevelFinisher levelFinisher)
    {
        _player = player;
        _levelFinisher = levelFinisher;
    }

    public void TransmitAndDestroy()
    {
        if (_player == null || _levelFinisher == null)
        {
            Debug.LogError("[FishLevelTransmitter] Dependencies are not set.");
            return;
        }
        
        if (_player.IsPlayerLevelMore(_fish.Level))
        {
            _audioClipChosen.PlayGoodAudioInPlace();
            _player.CatchFish(_fish.Level);
            _fish.CatchMe();
        }
        else
        {
            _audioClipChosen.PlayBadAudio();
            _levelFinisher.BadEnd();
        }
    }
}
