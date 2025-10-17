using UnityEngine;
using YG;

public class SkinShopUIController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SkinShop shopManager;      // Логика магазина (имеет ссылку на SkinLibrary)
    
    [Header("UI")]
    [SerializeField] private Transform gridRoot;        // Контейнер для карточек
    [SerializeField] private SkinItemView itemPrefab;   // Префаб карточки скина

    private SkinItemView[] _items;
    private SkinDefinition[] _defs;

    private void OnEnable()
    {
        YG2.onGetSDKData += OnSdkReady;
        YG2.onSwitchLang += OnLang;

        if (shopManager) 
            shopManager.OnSkinsChanged += RedrawAll;
    }

    private void OnDisable()
    {
        YG2.onGetSDKData -= OnSdkReady;
        YG2.onSwitchLang -= OnLang;

        if (shopManager) 
            shopManager.OnSkinsChanged -= RedrawAll;
    }

    private void Start()
    {
        // В редакторе SDK может быть уже инициализирован
        if (!string.IsNullOrEmpty(YG2.lang))
            Build();
    }

    private void OnSdkReady() => Build();
    private void OnLang(string _) => RedrawAll();

    public void Build()
    {
        if (!gridRoot || !itemPrefab || shopManager == null)
        {
            Debug.LogWarning("[ShopUI] Проверь ссылки: gridRoot, itemPrefab, shopManager");
            return;
        }

        // Получаем список скинов через SkinShop (он сам возьмёт их из SkinLibrary и отсортирует)
        _defs = shopManager.GetSkinsSorted();
        if (_defs == null || _defs.Length == 0)
        {
            Debug.LogWarning("[ShopUI] В библиотеке скинов ничего нет");
            return;
        }

        // Очищаем контейнер
        for (int i = gridRoot.childCount - 1; i >= 0; i--)
            Destroy(gridRoot.GetChild(i).gameObject);

        // Создаём карточки
        _items = new SkinItemView[_defs.Length];

        for (int i = 0; i < _defs.Length; i++)
        {
            var def = _defs[i];
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
