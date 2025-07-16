using System;
using UnityEngine;

public class Fish : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;

    public event Action<int> LevelSet;

    public int Level { get; private set; }

    private void OnEnable()
    {
        FishingStoper.OnFishingStop += PlayFX;
    }

    private void OnDisable()
    {
        FishingStoper.OnFishingStop -= PlayFX;
    }
    
    public void SetLevel(int level)
    {
        Level = level;
        LevelSet?.Invoke(Level);
    }

    private void PlayFX()
    {
        if (_particleSystem != null)
        {
            _particleSystem.gameObject.SetActive(true);
            _particleSystem.transform.parent = null;
            _particleSystem.Play();
        }
    }
}