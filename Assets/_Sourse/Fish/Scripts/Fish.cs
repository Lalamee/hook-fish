using System;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(FishMover))]
public class Fish : MonoBehaviour
{
    private const float AppearScaleUpDuration = 0.15f;
    private const float AppearScaleSettleDuration = 0.15f;
    private const float AppearScaleOvershoot = 1.12f;
    private const float DespawnGrowDuration = 0.2f;
    private const float DespawnCollapseDuration = 0.3f;
    private const float CatchPhaseDuration = 0.25f;
    private const float CatchCollapseDuration = 0.4f;

    [SerializeField] private FishMover _fishMover;
    [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;
    [SerializeField] private ParticleSystem _particleEnd;
    [SerializeField] private ParticleSystem _particleCatch;

    private Material _material;
    private Vector3 _initialScale;
    private bool _isDespawning;

    public event Action<int> LevelChanged;

    public int Level { get; private set; }

    private void OnEnable()
    {
        if (_skinnedMeshRenderer == null)
        {
            _skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>(true);
        }

        _material = _skinnedMeshRenderer.material;

        if (_fishMover == null)
        {
            _fishMover = GetComponent<FishMover>();
        }

        _fishMover.SetMovementAllowed(false);
        _initialScale = transform.localScale;
        _isDespawning = false;

        ResetParticle(_particleCatch);
        ResetParticle(_particleEnd);
        PlaySpawnAnimation();

        FishingStoper.OnFishingStop += Despawn;
    }

    private void OnDisable()
    {
        FishingStoper.OnFishingStop -= Despawn;
        DOTween.Kill(transform);

        if (_material != null)
        {
            DOTween.Kill(_material);
        }
    }

    public void SetLevel(int level)
    {
        Level = level;
        LevelChanged?.Invoke(Level);
    }

    public void Despawn()
    {
        if (_isDespawning)
        {
            return;
        }

        _isDespawning = true;
        _fishMover.SetMovementAllowed(false);
        DOTween.Kill(transform);

        PlayDetachedParticle(_particleEnd);

        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOScale(new Vector3(1.5f * _initialScale.x, 0.5f * _initialScale.y, _initialScale.z), CatchPhaseDuration).SetEase(Ease.InOutSine));
        sequence.Append(transform.DOScale(new Vector3(0.7f * _initialScale.x, 1.3f * _initialScale.y, _initialScale.z), CatchPhaseDuration).SetEase(Ease.InOutSine));
        sequence.Append(transform.DOScale(Vector3.zero, CatchCollapseDuration).SetEase(Ease.InBack));
        sequence.OnComplete(() => Destroy(gameObject));
    }

    public void Catch()
    {
        if (_isDespawning)
        {
            return;
        }

        _isDespawning = true;
        _fishMover.SetMovementAllowed(false);
        DOTween.Kill(transform);

        PlayDetachedParticle(_particleCatch);

        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOScale(1.2f * _initialScale, DespawnGrowDuration));
        sequence.Append(transform.DOScale(Vector3.zero, DespawnCollapseDuration).SetEase(Ease.InBack));
        sequence.OnComplete(() => Destroy(gameObject));
    }

    [Obsolete("Use Despawn instead.")]
    public void DestroyMe()
    {
        Despawn();
    }

    [Obsolete("Use Catch instead.")]
    public void CatchMe()
    {
        Catch();
    }

    private void PlaySpawnAnimation()
    {
        transform.localScale = Vector3.zero;

        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOScale(_initialScale * AppearScaleOvershoot, AppearScaleUpDuration).SetEase(Ease.OutQuad));
        sequence.Append(transform.DOScale(_initialScale, AppearScaleSettleDuration).SetEase(Ease.InOutSine));
        sequence.OnComplete(() => _fishMover.SetMovementAllowed(true));
    }

    private void PlayDetachedParticle(ParticleSystem particleSystem)
    {
        if (particleSystem == null)
        {
            return;
        }

        Transform particleTransform = particleSystem.transform;
        particleTransform.SetParent(null, true);

        GameObject particleObject = particleSystem.gameObject;
        particleObject.SetActive(true);
        particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        particleSystem.Play(true);

        ParticleSystem.MainModule mainModule = particleSystem.main;
        float lifetime = mainModule.duration + mainModule.startLifetime.constantMax;
        Destroy(particleObject, lifetime);
    }

    private void ResetParticle(ParticleSystem particleSystem)
    {
        if (particleSystem == null)
        {
            return;
        }

        particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        particleSystem.gameObject.SetActive(false);
    }
}
