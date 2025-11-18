using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUI : MonoBehaviour
{
    [SerializeField] private UIInGame _inGame;
    [SerializeField] private Setting _setting;
    [SerializeField] private GoodEnd _goodEnd;
    [SerializeField] private BadEnd _badEnd;

    public void Initialize()
    {
        _inGame.gameObject.SetActive(true);
        _setting.gameObject.SetActive(false);
        _goodEnd.TurnOffObject();
        _badEnd.TurnOffObject();
    }
}
