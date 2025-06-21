using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    private Player _player;
    private BoatMover _boat;
    private HarpoonControl _harpoon;
    private Hook _hook;
    private Laser _laser;
    private GoodEnd _goodEnd;
    private BadEnd _badEnd;
    private FishingStoper[] _fishingStopers;
    private Spawner[] _spawners;

    private void Start()
    {
        Time.timeScale = 1;
        _player = FindObjectOfType<Player>();
        _boat = FindObjectOfType<BoatMover>();
        _harpoon = FindObjectOfType<HarpoonControl>();
        _hook = FindObjectOfType<Hook>();
        _laser = FindObjectOfType<Laser>();
        _boat.enabled = true;
        _harpoon.enabled = false;
        _hook.enabled = false;

    }
}
