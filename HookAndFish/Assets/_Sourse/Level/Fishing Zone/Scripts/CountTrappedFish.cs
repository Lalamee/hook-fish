using TMPro;
using UnityEngine;
using DG.Tweening;

public class CountTrappedFish : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TMP_Text _count;

    private int _needCountFish = 3;
    private int _currentCount = 0;

    private void Start()
    {
        _count.text = "0/" + _needCountFish.ToString();
    }

    private void OnEnable()
    {
        _player.CountTrappedFishChange += OnCountChange;
    }

    private void OnDisable()
    {
        _player.CountTrappedFishChange -= OnCountChange;
    }

    private void OnCountChange(int newCount)
    {
        _currentCount = newCount;
        
        _count.text = newCount.ToString() + "/" + _needCountFish.ToString();
        
        _count.transform
            .DOPunchScale(Vector3.one * 0.25f, 0.35f, 8, 0.6f)
            .SetId(_count.transform);
    }
}