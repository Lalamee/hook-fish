using System;
using UnityEngine;

[RequireComponent(typeof(FishingZone))]
public class FishingStoper : MonoBehaviour
{
    public static event Action OnFishingStop;

    private Player _player;
    private BoatMover _boatMover;
    private HarpoonControl _harpoon;
    private Hook _hook;
    private Laser _laser;
    private int _countTrappedFish;
    private int _needCountFish = 3;
    
    private void Update()
    {
        _countTrappedFish = _player.GetCountTrappedFish();

        if (_countTrappedFish == _needCountFish)
            StopFishing();
    }

    public void Initialize(Player player, BoatMover boatMover, HarpoonControl harpoon, Hook hook, Laser laser)
    {
        _player = player;
        _boatMover = boatMover;
        _harpoon = harpoon;
        _hook = hook;
        _laser = laser;
    }

    private void StopFishing()
    {
        OnFishingStop?.Invoke();

        _laser?.OffRenderer();
        _player?.EndFishInZone();

        if (_boatMover != null)
            _boatMover.enabled = true;

        if (_harpoon != null)
            _harpoon.enabled = false;

        if (_hook != null)
            _hook.enabled = false;

        Destroy(gameObject);
    }
}