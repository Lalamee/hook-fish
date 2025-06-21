using TMPro;
using UnityEngine;


public class CountTrappedFish : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TMP_Text _count;
    
    private int _needCountFish = 3;

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

    private void OnCountChange(int count)
    {
        _count.text = count.ToString() + '/' + _needCountFish.ToString();
    }
}