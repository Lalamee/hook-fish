using UnityEngine;
using YG;
using TMPro;

public class LanguageSwitcher : MonoBehaviour
{
    public string ru, en, tr;

    private TextMeshProUGUI textComponent;

    private void Awake()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
        SwitchLanguage(YG2.lang);
    }

    private void OnEnable()
    {
        YG2.onSwitchLang += SwitchLanguage;
    }

    private void OnDisable()
    {
        YG2.onSwitchLang -= SwitchLanguage;
    }

    public void SwitchLanguage(string lang)
    {
        switch (lang)
        {
            case "ru":
                textComponent.text = ru;
                break;
            case "tr":
                textComponent.text = tr;
                break;
            default:
                textComponent.text = en;
                break;
        }
    }
}
