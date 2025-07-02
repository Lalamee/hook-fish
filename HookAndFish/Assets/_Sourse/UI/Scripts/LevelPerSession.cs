using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class LevelPerSession : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private LanguageSwitcher _languageText;

    private void OnEnable()
    {
        _languageText.baseText = '+' + _player.GetLevel().ToString() + " ";
    }
}
