using UnityEngine;
using DG.Tweening;

public class BoatRecoil : MonoBehaviour
{
    [SerializeField] private float _shakeDuration = 0.25f;
    [SerializeField] private float _shakeStrength = 5f;   
    [SerializeField] private int _vibrato = 8;
    [SerializeField] private float _randomness = 90f;

    private Tween _shakeTween;
    
    public void DoRecoil()
    {
        _shakeTween?.Kill();
        
        _shakeTween = transform.DOShakeRotation(
            _shakeDuration,  
            _shakeStrength,   
            _vibrato,        
            _randomness,     
            true      
        );
    }
}