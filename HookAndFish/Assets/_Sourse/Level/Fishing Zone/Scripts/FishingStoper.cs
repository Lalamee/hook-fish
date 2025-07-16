using UnityEngine;
using System;

[RequireComponent(typeof(FishingZone))]
public class FishingStoper : MonoBehaviour
{
    public static event Action OnFishingStop;

    private Player _player;
    private BoatMover _boat;
    private HarpoonControl _harpoon;
    private Hook _hook;
    private Laser _laser;
    private int _countTrappedFish;
    private int _needCountFish = 3;

    private void Start()
    {
        _player = FindObjectOfType<Player>();
        _boat = FindObjectOfType<BoatMover>();
        _harpoon = FindObjectOfType<HarpoonControl>();
        _hook = FindObjectOfType<Hook>();
        _laser = FindObjectOfType<Laser>();
    }

    private void Update()
    {
        _countTrappedFish = _player.GetCountTrappedFish();

        if (_countTrappedFish == _needCountFish)
            StopFishing();
    }

    private void StopFishing()
    {
        OnFishingStop?.Invoke(); 

        _laser.OffRenderer();
        _player.SetNewStartLevel();
        _player.ResetCountTrappedFish();
        _boat.enabled = true;
        _harpoon.enabled = false;
        _hook.enabled = false;

        Destroy(gameObject);
    }
}