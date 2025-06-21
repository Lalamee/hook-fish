using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    private int _level;
    private int _startLevel;
    private int _countTrappedFish;

    public event UnityAction<int> LevelChange;
    public event UnityAction<int> CountTrappedFishChange;

    private void Start()
    {
        _countTrappedFish = 0;
        _level = 1;
        _startLevel = _level;
        LevelChange?.Invoke(_level);
    }

    public void CatchFish(int fishLevel)
    {
        _level += fishLevel;
        _countTrappedFish++;
        CountTrappedFishChange?.Invoke(_countTrappedFish);
        LevelChange?.Invoke(_level);
    }

    public bool IsPlayerLevelMore(int fishLevel)
    {
        if (_level < fishLevel)
            return false;
        
        return true;
    }

    public int GetLevel()
    {
        return _level;
    }

    public int GetStartLevel()
    {
        return _startLevel;
    }

    public void SetNewStartLevel()
    {
        _startLevel = _level;
    }

    public int GetCountTrappedFish()
    {
        return _countTrappedFish;
    }

    public void ResetCountTrappedFish()
    {
        _countTrappedFish = 0;
    }
}
