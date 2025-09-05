using UnityEngine;
using DG.Tweening;

public class LeaderboardPopup : MonoBehaviour
{
    [SerializeField] private RectTransform popupContainer;
    [SerializeField] private RectTransform exitButton;
    [SerializeField] private float showDuration = 0.3f;
    [SerializeField] private float hideDuration = 0.22f;

    Vector3 popupInitialScale, exitInitialScale;
    Tween showTween, hideTween;

    void Awake()
    {
        popupInitialScale = popupContainer ? popupContainer.localScale : Vector3.one;
        exitInitialScale  = exitButton    ? exitButton.localScale    : Vector3.one;
    }

    void OnEnable()
    {
        showTween?.Kill();
        hideTween?.Kill();

        if (popupContainer)
        {
            popupContainer.localScale = Vector3.zero;
            showTween = popupContainer.DOScale(popupInitialScale, showDuration).SetEase(Ease.OutBack);
        }

        if (exitButton)
        {
            exitButton.localScale = Vector3.zero;
            exitButton.DOScale(exitInitialScale, showDuration).SetEase(Ease.OutBack).SetDelay(0.08f);
        }
    }

    public void Close()
    {
        showTween?.Kill();
        hideTween?.Kill();

        Tween t1 = popupContainer
            ? popupContainer.DOScale(Vector3.zero, hideDuration).SetEase(Ease.InBack)
            : null;

        Tween t2 = exitButton
            ? exitButton.DOScale(Vector3.zero, hideDuration * 0.9f).SetEase(Ease.InBack)
            : null;

        hideTween = DOTween.Sequence()
            .Join(t1)
            .Join(t2)
            .OnComplete(() => gameObject.SetActive(false));
    }
}