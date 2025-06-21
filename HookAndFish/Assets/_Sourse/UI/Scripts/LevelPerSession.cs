using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class LevelPerSession : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TMP_Text _level;

    private void OnEnable()
    {
        _level.text = '+' + _player.GetLevel().ToString() + " Lvl";
    }
}
