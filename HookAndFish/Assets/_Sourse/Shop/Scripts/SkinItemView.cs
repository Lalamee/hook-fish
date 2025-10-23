using UnityEngine;
using UnityEngine.UI;
using TMPro;
using YG;

public enum SkinItemState { LockedLevel, LockedAd, Unlocked, Selected }

public class SkinItemView : MonoBehaviour
{
    [SerializeField] private Image playerImage;
    [SerializeField] private GameObject lockImage;
    [SerializeField] private Transform priceRoot;
    [SerializeField] private TMP_Text priceText;
    [SerializeField] private GameObject selectedBadge;
    [SerializeField] private TMP_Text selectedText;
    [SerializeField] private Button button;
    [SerializeField] private Image adIcon;

    [SerializeField] private LanguageSwitcher priceTextLang;
    [SerializeField] private LanguageSwitcher selectedTextLang;

    private SkinDefinition _def;
    private SkinShop _shop;
    private System.Action _onAnyAction;

    public void Bind(SkinDefinition def, SkinShop shop, System.Action onAnyAction)
    {
        _def = def;
        _shop = shop;
        _onAnyAction = onAnyAction;

        if (!button) button = GetComponent<Button>();
        if (!priceText && priceRoot) priceText = priceRoot.GetComponentInChildren<TMP_Text>(true);
        if (!priceTextLang && priceText) priceTextLang = priceText.GetComponent<LanguageSwitcher>();
        if (!selectedTextLang && selectedText) selectedTextLang = selectedText.GetComponent<LanguageSwitcher>();

        if (playerImage)
        {
            playerImage.sprite = _def.icon;
            playerImage.preserveAspect = true;
            playerImage.enabled = (_def.icon != null);
        }

        MakeOverlayNonBlocking(lockImage);

        if (button)
        {
            button.enabled = true;
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(OnClick);
        }

        if (selectedText && selectedTextLang)
        {
            selectedTextLang.baseText = "";
            selectedTextLang.ru = "Выбрано";
            selectedTextLang.en = "Selected";
            selectedTextLang.tr = "Seçildi";
            selectedTextLang.UpdateText();
        }
        else if (selectedText)
        {
            selectedText.text = "Selected";
        }

        if (_shop != null)
        {
            _shop.OnSkinsChanged -= OnShopChanged;
            _shop.OnSkinsChanged += OnShopChanged;
        }

        Redraw();
    }

    private void OnDestroy()
    {
        if (_shop != null) _shop.OnSkinsChanged -= OnShopChanged;
    }

    private void OnShopChanged()
    {
        Redraw();
        _onAnyAction?.Invoke();
    }

    public void Redraw()
    {
        bool unlocked = YG2.saves.IsSkinUnlocked(_def.id);
        bool selected = unlocked && YG2.saves.selectedSkinId == _def.id;

        SkinItemState state = !unlocked
            ? (_def.unlockType == SkinUnlockType.Level ? SkinItemState.LockedLevel : SkinItemState.LockedAd)
            : (selected ? SkinItemState.Selected : SkinItemState.Unlocked);

        if (lockImage) lockImage.SetActive(state == SkinItemState.LockedLevel || state == SkinItemState.LockedAd);
        if (priceRoot) priceRoot.gameObject.SetActive(state == SkinItemState.LockedLevel || state == SkinItemState.LockedAd);
        if (selectedBadge) selectedBadge.SetActive(state == SkinItemState.Selected);

        if (adIcon)
        {
            bool showAdIcon = state == SkinItemState.LockedAd;
            adIcon.gameObject.SetActive(showAdIcon);
        }

        if (state == SkinItemState.LockedLevel) SetPriceText_Level(_def.requiredLevel);
        else if (state == SkinItemState.LockedAd) SetPriceText_WatchAd();

        if (button)
        {
            bool canClick =
                (state == SkinItemState.LockedLevel && YG2.saves.playerLevel >= _def.requiredLevel)
                || state == SkinItemState.LockedAd
                || state == SkinItemState.Unlocked;

            button.interactable = canClick;
        }
    }

    private void OnClick()
    {
        bool unlocked = YG2.saves.IsSkinUnlocked(_def.id);

        if (!unlocked)
        {
            if (_def.unlockType == SkinUnlockType.Level)
            {
                if (YG2.saves.playerLevel >= _def.requiredLevel)
                    _shop.TryUnlockLevelSkin(_def.id);
            }
            else
            {
                _shop.TryUnlockRewardedSkin(_def.id);
            }
        }
        else
        {
            _shop.TrySelectSkin(_def.id);
        }

        Redraw();
    }

    private void SetPriceText_Level(int level)
    {
        if (priceTextLang != null)
        {
            priceTextLang.baseText = level.ToString();
            priceTextLang.ru = "Ур.";
            priceTextLang.en = "Lvl";
            priceTextLang.tr = "Sev.";
            priceTextLang.UpdateText();
        }
        else if (priceText) priceText.text = $"Lvl {level}";
    }

    private void SetPriceText_WatchAd()
    {
        if (priceTextLang != null)
        {
            priceTextLang.baseText = "";
            priceTextLang.ru = "Смотреть рекламу";
            priceTextLang.en = "Watch Ad";
            priceTextLang.tr = "Reklam İzle";
            priceTextLang.UpdateText();
        }
        else if (priceText) priceText.text = "Watch Ad";
    }

    private void MakeOverlayNonBlocking(GameObject go)
    {
        if (!go) return;
        var cg = go.GetComponent<CanvasGroup>();
        if (!cg) cg = go.AddComponent<CanvasGroup>();
        cg.blocksRaycasts = false;
        foreach (var g in go.GetComponentsInChildren<Graphic>(true))
            g.raycastTarget = false;
    }
}
