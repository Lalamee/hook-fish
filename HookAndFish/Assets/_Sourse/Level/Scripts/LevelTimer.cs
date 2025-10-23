using UnityEngine;
using TMPro;
using DG.Tweening;

public class LevelTimer : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;

    [Header("Level Time")]
    [SerializeField] private int _levelMinutes = 0;
    [SerializeField] private int _levelSeconds = 30;

    [Header("Critical & End Settings")]
    [SerializeField] private float _criticalTime = 10f;
    [SerializeField] private float _pulseStartTime = 5f;

    [Header("Bonus FX")]
    [SerializeField] private int _bonusSeconds = 5;
    [SerializeField] private float _colorInDuration = 0.15f;
    [SerializeField] private float _colorOutDuration = 0.25f;
    [SerializeField] private float _bonusScale = 1.12f;
    [SerializeField] private float _bonusScaleInDuration = 0.18f;
    [SerializeField] private float _bonusScaleOutDuration = 0.18f;

    [Header("Pulse FX")]
    [SerializeField] private float _pulseScale = 1.1f;
    [SerializeField] private float _pulseSpeed = 0.5f;
    [SerializeField] private float _resetScaleDuration = 0.1f;

    private LevelFinisher _finisher;
    private float _time;
    private Color _baseColor;
    private Vector3 _baseScale;
    private Tween _pulseTween;

    void OnEnable()  => FishingStoper.OnFishingStop += OnFishingStop;
    void OnDisable() => FishingStoper.OnFishingStop -= OnFishingStop;

    void Awake()
    {
        if (!timerText)
        {
            Debug.LogError("[LevelTimer] TMP_Text reference missing.");
            enabled = false;
            return;
        }

        _baseColor = timerText.color;
        _baseScale = timerText.rectTransform.localScale;
    }

    void Start()
    {
        _finisher = FindObjectOfType<LevelFinisher>();
        _time = _levelMinutes * 60 + _levelSeconds;
        Redraw();
    }

    void Update()
    {
        _time -= Time.deltaTime;
        if (_time < 0f) _time = 0f;

        Redraw();

        if (_time <= _pulseStartTime)
            StartPulse();
        else
            StopPulse();

        if (_time <= 0f)
            _finisher.BadEnd();
    }

    void Redraw()
    {
        int m = Mathf.FloorToInt(_time / 60f);
        int s = Mathf.FloorToInt(_time % 60f);
        timerText.text = $"{m:00}:{s:00}";

        if (!DOTween.IsTweening(timerText, true))
            timerText.color = (_time <= _criticalTime) ? Color.red : _baseColor;
    }

    void OnFishingStop()
    {
        _time += _bonusSeconds;
        Redraw();
        StopPulse();

        timerText.DOKill();
        timerText.rectTransform.DOKill();

        timerText.DOColor(Color.green, _colorInDuration).SetUpdate(true).OnComplete(() =>
        {
            var targetColor = (_time <= _criticalTime) ? Color.red : _baseColor;
            timerText.DOColor(targetColor, _colorOutDuration).SetUpdate(true);
        });

        timerText.rectTransform
            .DOScale(_bonusScale, _bonusScaleInDuration)
            .SetEase(Ease.OutBack)
            .SetUpdate(true)
            .OnComplete(() =>
                timerText.rectTransform.DOScale(_baseScale, _bonusScaleOutDuration)
                .SetEase(Ease.InOutSine)
                .SetUpdate(true));
    }

    void StartPulse()
    {
        if (_pulseTween != null && _pulseTween.IsActive()) return;

        _pulseTween = timerText.rectTransform
            .DOScale(_pulseScale, _pulseSpeed)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo)
            .SetUpdate(true);
    }

    void StopPulse()
    {
        if (_pulseTween == null || !_pulseTween.IsActive()) return;

        _pulseTween.Kill();
        timerText.rectTransform
            .DOScale(_baseScale, _resetScaleDuration)
            .SetUpdate(true);
    }
}
