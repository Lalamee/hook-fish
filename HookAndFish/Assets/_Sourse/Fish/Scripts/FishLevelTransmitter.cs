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
        _fish = GetComponent<Fish>();
        _player = FindObjectOfType<Player>();
        _levelFinisher = FindObjectOfType<LevelFinisher>();
    }

    public void TransmitAndDestroy()
    {
        if (_player.IsPlayerLevelMore(_fish.Level))
        {
            _player.CatchFish(_fish.Level);
            _audioClipChosen.PlayGoodAudio();
                Destroy(_fish.gameObject);
        }
        else
        {
            _audioClipChosen.PlayGoodAudio();
            _levelFinisher.BadEnd();
        }
    }
}
