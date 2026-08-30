using TMPro;
using UnityEngine;


public class FishLevel : MonoBehaviour
{
    [SerializeField] private Fish _fish;
    [SerializeField] private LanguageSwitcher _languageText;
    
    private void OnEnable()
    {
        _fish.LevelChanged += OnLevelChanged;
    }

    private void OnDisable()
    {
        _fish.LevelChanged -= OnLevelChanged;
    }

    private void OnLevelChanged(int level)
    {
        _languageText.baseText = level.ToString();
    }
}
