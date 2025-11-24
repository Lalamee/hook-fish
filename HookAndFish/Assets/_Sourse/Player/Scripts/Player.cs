using System;
using UnityEngine;
using YG;

public class Player : MonoBehaviour
{
    [SerializeField] AudioClipChosen _audioClipChosen;
    
    private int _level;
    private int _startLevel;
    private int _countTrappedFish;

    public event Action<int> LevelChange;
    public event Action<int> CountTrappedFishChange;

    private void OnEnable()
    {
        FishingStoper.OnFishingStop += EndFishInZone;
    }

    private void OnDisable()
    {
        FishingStoper.OnFishingStop -= EndFishInZone;
    }
    
    private void Start()
    {
        _countTrappedFish = 0;
        _level = YG2.saves.playerLevel;
        _startLevel = _level;
        LevelChange?.Invoke(_level);
    }

    public void CatchFish(int fishLevel)
    {
        int levelStep = 4;
        
        if(_level == fishLevel && fishLevel == 1)
            _level += fishLevel;
        else if(_level == fishLevel)
            _level += levelStep;
        else
            _level++;
        
        _countTrappedFish++;
        _audioClipChosen.PlayGoodAudio();
        
        CountTrappedFishChange?.Invoke(_countTrappedFish);
        LevelChange?.Invoke(_level);
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
    
    public void EndFishInZone()
    {
        _countTrappedFish = 0;
    }
}