using UnityEngine;
using YG;

public class SkinShopUIController : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private SkinDefinition[] skins;
    [SerializeField] private SkinShop shopManager;

    [Header("UI")]
    [SerializeField] private Transform gridRoot;      // Viewport/Content
    [SerializeField] private SkinItemView itemPrefab; // Character Skin Item prefab

    private SkinItemView[] _items;

    private void OnEnable()
    {
        YG2.onGetSDKData += OnSdkReady;
        YG2.onSwitchLang += OnLang;

        if (shopManager) shopManager.OnSkinsChanged += RedrawAll;
    }

    private void OnDisable()
    {
        YG2.onGetSDKData -= OnSdkReady;
        YG2.onSwitchLang -= OnLang;

        if (shopManager) shopManager.OnSkinsChanged -= RedrawAll;
    }

    private void Start()
    {
        // в редакторе SDK может быть уже инициализирован
        if (!string.IsNullOrEmpty(YG2.lang)) Build();
    }

    private void OnSdkReady() => Build();
    private void OnLang(string _) => RedrawAll();

    public void Build()
    {
        if (!gridRoot || !itemPrefab || skins == null || skins.Length == 0)
        {
            Debug.LogWarning("[ShopUI] Проверь gridRoot/itemPrefab/skins");
            return;
        }

        for (int i = gridRoot.childCount - 1; i >= 0; i--)
            Destroy(gridRoot.GetChild(i).gameObject);

        _items = new SkinItemView[skins.Length];

        for (int i = 0; i < skins.Length; i++)
        {
            var def = skins[i];
            if (!def) continue;

            var view = Instantiate(itemPrefab, gridRoot, false);
            _items[i] = view;
            view.Bind(def, shopManager, RedrawAll);
        }
    }

    public void RedrawAll()
    {
        if (_items == null) return;
        foreach (var it in _items)
            if (it) it.SendMessage("Redraw", SendMessageOptions.DontRequireReceiver);
    }
}