using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUI : MonoBehaviour
{
    [SerializeField] Setting _setting;
    [SerializeField] GoodEnd _goodEnd;
    [SerializeField] BadEnd _badEnd;

    public void Initialize()
    {
        _setting.gameObject.SetActive(false);
        _goodEnd.TurnOffObject();
        _badEnd.TurnOffObject();
    }
}
