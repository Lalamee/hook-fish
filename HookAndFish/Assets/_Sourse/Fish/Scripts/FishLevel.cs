using TMPro;
using UnityEngine;


public class FishLevel : MonoBehaviour
{
    [SerializeField] private Fish _fish;
    [SerializeField] private LanguageSwitcher _languageText;
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
        _languageText.baseText = level.ToString();
    }
}
