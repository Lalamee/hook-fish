using UnityEngine;

[RequireComponent(typeof(Fish))]
public class FishLevelTransmitter : MonoBehaviour
{
    private Fish _fish;
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
            Destroy(_fish.gameObject);
        }
        else
        {
            _levelFinisher.BadEnd();
        }
    }
}
