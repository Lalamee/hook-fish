using UnityEngine;
using DG.Tweening;

public class BoatRecoil : MonoBehaviour
{
    [SerializeField] private float shakeDuration = 0.25f;
    [SerializeField] private float shakeStrength = 5f;   
    [SerializeField] private int vibrato = 8;
    [SerializeField] private float randomness = 90f;

    private Tween _shakeTween;
    
    public void DoRecoil()
    {
        _shakeTween?.Kill();
        
        _shakeTween = transform.DOShakeRotation(
            shakeDuration,  
            shakeStrength,   
            vibrato,        
            randomness,     
            true      
        );
    }
}