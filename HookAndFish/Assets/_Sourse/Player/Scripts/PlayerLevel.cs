using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class PlayerLevel : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TMP_Text _level;

    private void OnEnable()
    {
        _player.LevelChange += OnLevelChange;
    }

    private void OnDisable()
    {
        _player.LevelChange -= OnLevelChange;
    }

    public string GetLevel()
    {
        return _level.text;
    }                                      

    private void OnLevelChange(int level)
    {
        _level.text = "Lvl " + level.ToString();
    }
}