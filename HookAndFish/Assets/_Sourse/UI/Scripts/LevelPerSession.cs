using TMPro;
using UnityEngine;
using DG.Tweening;
using YG;

[RequireComponent(typeof(TMP_Text))]
public class LevelPerSession : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private LanguageSwitcher _languageText;

    [SerializeField] private float _countDuration = 0.6f;
    [SerializeField] private float _punchScale = 0.25f;
    [SerializeField] private float _punchDuration = 0.35f;
    [SerializeField] private Color _highlightColor = Color.yellow;

    private TMP_Text _text;
    private Color _defaultColor;
    private Tween _countTween;
    private Sequence _finishSequence;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
        _defaultColor = _text.color;

        if (_languageText == null)
            _languageText = GetComponent<LanguageSwitcher>();
    }

    private void OnEnable()
    {
        if (_player == null)
        {
            _player = FindObjectOfType<Player>();
            if (_player == null)
                return;
        }

        int sessionLevel = _player.GetLevel();

        YG2.saves.playerLevel += sessionLevel;
        YG2.SetLeaderboard("stats", YG2.saves.playerLevel);

        _countTween?.Kill();
        _finishSequence?.Kill();

        _text.transform.localScale = Vector3.one;
        _text.color = _defaultColor;

        int startValue = sessionLevel > 0 ? 1 : 0;
        SetValue(startValue);

        if (sessionLevel > 0)
        {
            _countTween = DOVirtual.Int(startValue, sessionLevel, _countDuration, value =>
            {
                SetValue(value);
            })
            .SetUpdate(true)
            .OnComplete(PlayFinishEffect);
        }
        else
        {
            PlayFinishEffect();
        }
    }

    private void SetValue(int value)
    {
        string numberPart = "+" + value.ToString();

        if (_languageText != null)
        {
            _languageText.baseText = numberPart;
            _languageText.UpdateText();
        }
        else
        {
            _text.text = numberPart;
        }
    }

    private void PlayFinishEffect()
    {
        _finishSequence = DOTween.Sequence();
        _finishSequence.SetUpdate(true);

        _finishSequence
            .Append(_text.DOColor(_highlightColor, _punchDuration * 0.5f))
            .Join(_text.transform.DOPunchScale(
                Vector3.one * _punchScale,
                _punchDuration,
                1,
                0.5f))
            .AppendInterval(0.1f)
            .Append(_text.DOColor(_defaultColor, 0.2f))
            .Join(_text.transform.DOScale(1f, 0.2f));
    }

    private void OnDisable()
    {
        _countTween?.Kill();
        _finishSequence?.Kill();
    }
}
