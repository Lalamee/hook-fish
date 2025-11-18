using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private Boat _boat;
    [SerializeField] private BoatMover _boatMover;
    [SerializeField] private LevelFinisher _levelFinisher;
    [SerializeField] private HarpoonControl _harpoon;
    [SerializeField] private Hook _hook;
    [SerializeField] private Laser _laser;
    [SerializeField] private Player _player;
    [SerializeField] private FishingStoper[] _fishingStopers;
    [SerializeField] private Spawner[] _spawners;
    [SerializeField] private AreaForBoat[] _areas;
    [SerializeField] private LevelUI _levelUI;
    [SerializeField] private LevelTimer _levelTimer;
    [SerializeField] private GoodEnd _goodEnd;
    [SerializeField] private BadEnd _badEnd;
    
    private void Awake()
    {
        foreach (var stoper in _fishingStopers)
        {
            stoper.Initialize(_player, _boatMover, _harpoon, _hook, _laser);
        }
    
        if (_levelFinisher != null)
            _levelFinisher.Initialize(_goodEnd, _badEnd);
    
        _levelUI.Initialize();
        
        if (_levelTimer != null)
            _levelTimer.Initialize(_levelFinisher);
    
        if (_boat != null)
            _boat.Initialize(_areas);
    
        if (_spawners == null)
            return;
    
        foreach (var spawner in _spawners)
        {
            if (spawner == null)
                continue;
    
            spawner.Initialize(_player, _boat, _levelFinisher);
        }
    }
}
