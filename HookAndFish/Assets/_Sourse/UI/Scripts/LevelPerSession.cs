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
        if(_player == null)
            _player = FindObjectOfType<Player>();
        
        Debug.Log(_player.GetLevel());
        Debug.Log( _player.GetStartLevel());

        int receivedLevel = _player.GetLevel() - _player.GetStartLevel();
        
        _languageText.baseText = '+' + receivedLevel.ToString() + " ";
        
        YG2.saves.playerLevel += receivedLevel;
        YG2.SetLeaderboard("stats",  YG2.saves.playerLevel);

    }
}