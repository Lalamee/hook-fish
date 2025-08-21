using TMPro;
using UnityEngine;
using YG;

[RequireComponent(typeof(TMP_Text))]
public class LevelPerSession : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private LanguageSwitcher _languageText;

    private void OnEnable()
    {
        _languageText.baseText = '+' + _player.GetLevel().ToString() + " ";
        
        YG2.saves.playerLevel += _player.GetLevel();
        YG2.SetLeaderboard("stats",  YG2.saves.playerLevel);

    }
}
