using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatInit : MonoBehaviour
{
    [SerializeField] private BoatMover _boat;
    [SerializeField] private HarpoonControl _harpoon;
    [SerializeField] private Hook _hook;

    private void Start()
    {
        Time.timeScale = 1;

        _boat.enabled = true;
        _harpoon.enabled = false;
        _hook.enabled = false;

    }
}
