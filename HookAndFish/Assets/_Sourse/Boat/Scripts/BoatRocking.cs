using UnityEngine;
using DG.Tweening;

public class BoatRocking : MonoBehaviour
{
    [SerializeField] private Transform boat;
    [SerializeField] private float angle = 3f;       
    [SerializeField] private float duration = 0.6f;  
    [SerializeField] private float stopReturnTime = 0.3f;

    private Tween rockingTween;
    
    public void StartRocking()
    {
        if (rockingTween != null && rockingTween.IsActive()) return;

        rockingTween = boat.DOLocalRotate(
                new Vector3(0, 0, angle),
                duration
            )
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }
    
    public void StopRocking()
    {
        if (rockingTween != null && rockingTween.IsActive())
            rockingTween.Kill();

        boat.DOLocalRotate(Vector3.zero, stopReturnTime).SetEase(Ease.OutSine);
    }
}