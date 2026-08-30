using UnityEngine;

public class BoatInit : MonoBehaviour
{
    [SerializeField] private BoatMover _boat;
    [SerializeField] private HarpoonControl _harpoon;
    [SerializeField] private Hook _hook;

    public void Init()
    {
        _boat.enabled = true;
        _harpoon.enabled = false;
        _hook.enabled = false;
    }
}
