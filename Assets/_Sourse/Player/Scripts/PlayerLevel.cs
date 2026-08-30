using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class PlayerLevel : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private LanguageSwitcher _languageText;

    private void Start()
    {
        OnLevelChanged(_player.GetLevel());
    }

    private void OnEnable()
    {
        _player.LevelChanged += OnLevelChanged;
    }

    private void OnDisable()
    {
        _player.LevelChanged -= OnLevelChanged;
    }

    private void OnLevelChanged(int level)
    {
        _languageText.baseText = level.ToString();
        _languageText.UpdateText();
    }
}
