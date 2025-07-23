using System;
using UnityEngine;

public class Fish : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleEnd;
    [SerializeField] private ParticleSystem _particleCatch;

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
        if (_particleEnd != null)
        {
            _particleEnd.gameObject.SetActive(true);
            _particleEnd.transform.parent = null;
            _particleEnd.Play();
        }
    }

    public void PlayCatchFX()
    {
        if (_particleCatch != null)
        {
            _particleCatch.gameObject.SetActive(true);
            _particleCatch.transform.parent = null;
            _particleCatch.Play();
        }
    }
}