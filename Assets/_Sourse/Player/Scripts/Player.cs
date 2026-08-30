using System;
using UnityEngine;
using YG;

public class Player : MonoBehaviour
{
    private const int FirstFishLevel = 1;
    private const int LevelStep = 4;

    [SerializeField] private AudioClipChosen _audioClipChosen;

    private int _level;
    private int _startLevel;
    private int _countTrappedFish;

    public event Action<int> LevelChanged;
    public event Action<int> TrappedFishCountChanged;

    private void OnEnable()
    {
        FishingStoper.OnFishingStop += OnFishingStopped;
    }

    private void OnDisable()
    {
        FishingStoper.OnFishingStop -= OnFishingStopped;
    }
    
    private void Start()
    {
        _countTrappedFish = 0;
        _level = YG2.saves.playerLevel;
        _startLevel = _level;
        LevelChanged?.Invoke(_level);
    }

    public void CatchFish(int fishLevel)
    {
        if (_level == fishLevel && fishLevel == FirstFishLevel)
        {
            _level += fishLevel;
        }
        else if (_level == fishLevel)
        {
            _level += LevelStep;
        }
        else
        {
            _level++;
        }

        _countTrappedFish++;
        _audioClipChosen.PlayGoodAudio();
        
        TrappedFishCountChanged?.Invoke(_countTrappedFish);
        LevelChanged?.Invoke(_level);
    }

    public bool IsPlayerLevelMore(int fishLevel)
    {
        return _level >= fishLevel;
    }

    public int GetLevel()
    {
        return _level;
    }

    public int GetStartLevel()
    {
        return _startLevel;
    }

    public int GetCountTrappedFish()
    {
        return _countTrappedFish;
    }
    
    public void OnFishingStopped()
    {
        _countTrappedFish = 0;
    }

    [Obsolete("Use OnFishingStopped instead.")]
    public void EndFishInZone()
    {
        OnFishingStopped();
    }
}
