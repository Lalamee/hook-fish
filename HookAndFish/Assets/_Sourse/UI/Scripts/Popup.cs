using UnityEngine;
using DG.Tweening;

public class Popup : MonoBehaviour
{
    [SerializeField] private RectTransform popupContainer;
    [SerializeField] private RectTransform exitButton;
    [SerializeField] private float showDuration = 0.30f;
    [SerializeField] private float hideDuration = 0.22f;

    private Vector3 popupInitialScale;
    private Vector3 exitInitialScale;

    private void Awake()
    {
        popupInitialScale = popupContainer ? popupContainer.localScale : Vector3.one;
        exitInitialScale  = exitButton    ? exitButton.localScale    : Vector3.one;
    }

    public void PlayShow()
    {
        if (popupContainer)
        {
            popupContainer.localScale = Vector3.zero;
            popupContainer.DOScale(popupInitialScale, showDuration)
                .SetEase(Ease.OutBack)
                .SetUpdate(true); // работает при timeScale=0
        }

        if (exitButton)
        {
            exitButton.localScale = Vector3.zero;
            exitButton.DOScale(exitInitialScale, showDuration)
                .SetEase(Ease.OutBack)
                .SetDelay(0.08f)
                .SetUpdate(true);
        }
    }

    public void PlayHide(System.Action onComplete)
    {
        Sequence s = DOTween.Sequence().SetUpdate(true);

        if (popupContainer)
            s.Join(popupContainer.DOScale(Vector3.zero, hideDuration).SetEase(Ease.InBack));

        if (exitButton)
            s.Join(exitButton.DOScale(Vector3.zero, hideDuration * 0.9f).SetEase(Ease.InBack));

        s.OnComplete(() => onComplete?.Invoke());
    }
}