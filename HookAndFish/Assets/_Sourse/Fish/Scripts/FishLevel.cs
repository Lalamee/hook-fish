using TMPro;
using UnityEngine;


public class FishLevel : MonoBehaviour
{
    [SerializeField] private Fish _fish;
    [SerializeField] private TMP_Text _level;

    private void OnEnable()
    {
        _fish.LevelSet += OnLevelSet;
    }

    private void OnDisable()
    {
        _fish.LevelSet -= OnLevelSet;
    }

    private void OnLevelSet(int level)
    {
        _level.text = level.ToString() + " Level";
    }
}
