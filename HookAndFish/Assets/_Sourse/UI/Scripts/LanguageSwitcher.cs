using System;
using UnityEngine;
using TMPro;
using YG;

public class LanguageSwitcher : MonoBehaviour
{
    public string ru, en, tr;
    public string baseText;

    private TMP_Text textComponent;
    private string lastAddedText = "";
    private string currentLang;

    private void Awake()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
        currentLang = YG2.lang;
    }

    private void Start()
    {
        UpdateText();
    }

    private void OnEnable()
    {
        YG2.onSwitchLang += OnLanguageChanged;
        OnLanguageChanged(YG2.lang);
    }

    private void OnDisable()
    {
        YG2.onSwitchLang -= OnLanguageChanged;
    }

    private void OnLanguageChanged(string lang)
    {
        currentLang = lang;
        UpdateText();
    }

    public void UpdateText()
    {
        switch (currentLang)
        {
            case "ru":
                lastAddedText = " " + ru;
                break;
            case "tr":
                lastAddedText = " " + tr;
                break;
            default:
                lastAddedText = " " + en;
                break;
        }
        textComponent.text = baseText + lastAddedText;
    }
}