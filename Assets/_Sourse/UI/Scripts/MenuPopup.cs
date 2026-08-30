using UnityEngine;
using TMPro;
using DG.Tweening;

public class MenuPopup : MonoBehaviour
{
    [Header("Top")]
    [SerializeField] private RectTransform languagesContainer;
    [SerializeField] private RectTransform[] languageFlags;
    [SerializeField] private TMP_Text titleText;

    [Header("Lower")]
    [SerializeField] private RectTransform lowerContainer;
    [SerializeField] private RectTransform[] lowerItems;

    [Header("Timings")]
    [SerializeField] private float moveTime = 0.4f;
    [SerializeField] private float popTime = 0.25f;
    [SerializeField] private float stagger = 0.1f;

    private void Start() => PlayIntro();

    public void PlayIntro()
    {
        var sequence = DOTween.Sequence();

        AnimateGroup(sequence, languagesContainer, languageFlags, Vector2.up * 120f);
        AnimateTitle(sequence);
        AnimateGroup(sequence, lowerContainer, lowerItems, Vector2.zero);
    }

    private void AnimateGroup(Sequence seq, RectTransform container, RectTransform[] items, Vector2 offset)
    {
        Vector2 p = container.anchoredPosition;
        container.anchoredPosition = p + offset;
        seq.Append(container.DOAnchorPos(p, moveTime).SetEase(Ease.OutCubic));

        for (int i = 0; i < items.Length; i++)
        {
            items[i].localScale = Vector3.zero;
            seq.Join(items[i].DOScale(1f, popTime).SetEase(Ease.OutBack).SetDelay(i * stagger));
        }
    }

    private void AnimateTitle(Sequence seq)
    {
        var rt = titleText.rectTransform;
        Vector2 p = rt.anchoredPosition;
        rt.anchoredPosition = p + Vector2.up * 80f;
        titleText.alpha = 0f;

        seq.Append(rt.DOAnchorPos(p, moveTime).SetEase(Ease.OutCubic));
        seq.Join(titleText.DOFade(1f, moveTime));
        seq.Join(rt.DOScale(1.05f, popTime).From(0.8f).SetEase(Ease.OutBack));
    }
}