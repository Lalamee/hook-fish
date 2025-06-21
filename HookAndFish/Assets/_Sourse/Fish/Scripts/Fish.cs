using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Fish : MonoBehaviour
{
    public event UnityAction<int> LevelSet;

    public int Level { get; private set; }

    private Player _player;
    private int _firstLevel;
    private int _levelStep;

    private void Start()
    {
        _player = FindObjectOfType<Player>();
        _firstLevel = 1;
        SetLevel();

        LevelSet?.Invoke(Level);
    }

    private void SetLevel()
    {
        float maxValue = 10.0f;
        float minValue = 0.0f;
        float halfValue = 7.0f;

        float valueForSet = Random.Range(minValue, maxValue);

        if (valueForSet <= halfValue)
        {
            if (_player.GetStartLevel() <= _firstLevel)
                _firstLevel = _player.GetStartLevel();
        
            _levelStep = 5;
            Level = Random.Range(_firstLevel, _player.GetLevel() + _levelStep);
        }
        else
        {
            Level = _player.GetLevel();
        }
    }
}
