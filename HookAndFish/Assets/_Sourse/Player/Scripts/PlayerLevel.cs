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
        OnLevelChange(_player.GetLevel());
    }

    private void OnEnable()
    {
        _player.LevelChange += OnLevelChange;
    }

    private void OnDisable()
    {
        _player.LevelChange -= OnLevelChange;
    }

    private void OnLevelChange(int level)
    {
        _languageText.baseText = level.ToString();
        _languageText.UpdateText();
    }
}