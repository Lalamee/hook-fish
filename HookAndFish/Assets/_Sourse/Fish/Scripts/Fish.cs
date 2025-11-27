using System;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(FishMover))]
public class Fish : MonoBehaviour
{
    [SerializeField] private FishMover _fishMover;
    [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;
    [SerializeField] private ParticleSystem _particleEnd;
    [SerializeField] private ParticleSystem _particleCatch;

    private float _appearScaleUp = 0.15f;
    private float _appearScaleSettle = 0.15f;
    private float _appearOvershoot = 1.12f;
    private float _vanishGrow = 0.2f;
    private float _vanishCollapse = 0.3f;
    private float _vanishOvershootScale = 1.2f;

    private Material _material;
    private Vector3 _initialScale;

    public event Action<int> LevelSet;
    public int Level { get; private set; }

    private void OnEnable()
    {
        if (_skinnedMeshRenderer == null)
            _skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>(true);

        _material = _skinnedMeshRenderer.material;

        if (_fishMover == null)
            _fishMover = GetComponent<FishMover>();

        _fishMover.SetMovementAllowed(false);

        _initialScale = transform.localScale;

        SpawnMe();

        FishingStoper.OnFishingStop += DestroyMe;
    }

    private void OnDisable()
    {
        FishingStoper.OnFishingStop -= DestroyMe;
        
        DOTween.Kill(transform);
        
        if (_material != null) 
            DOTween.Kill(_material);
    }

    public void SetLevel(int level)
    {
        Level = level;
        LevelSet?.Invoke(Level);
    }

    public void DestroyMe()
    {
        _fishMover.SetMovementAllowed(false);
        
        DOTween.Kill(gameObject);
        
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOScale(_vanishOvershootScale * _initialScale, _vanishGrow));
        sequence.Append(transform.DOScale(Vector3.zero, _vanishCollapse).SetEase(Ease.InBack));
        sequence.OnComplete(() => Destroy(gameObject));
        
        PlayAndDetach(_particleEnd);
    }

    public void CatchMe()
    {
        DestroyMe();
    }

    private void SpawnMe()
    {
        transform.localScale = Vector3.zero;

        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOScale(_initialScale * _appearOvershoot, _appearScaleUp).SetEase(Ease.OutQuad));
        sequence.Append(transform.DOScale(_initialScale, _appearScaleSettle).SetEase(Ease.InOutSine));
        sequence.OnComplete(() => _fishMover.SetMovementAllowed(true));
    }

    private void PlayAndDetach(ParticleSystem particleSystem)
    {
        if (particleSystem == null)
            return;

        particleSystem.gameObject.SetActive(true);
        particleSystem.Play();
        particleSystem.gameObject.SetActive(false);
    }
}
