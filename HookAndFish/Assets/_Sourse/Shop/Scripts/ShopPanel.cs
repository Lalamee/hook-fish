using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPanel : MonoBehaviour
{
    private List<ShopItemView> _shopItems = new List<ShopItemView>();

    [SerializeField] private Transform _itemParent;
    [SerializeField] private ShopItemViewFactory _shopItemViewFactory;

    public void Show(IEnumerable<ShopItem> items)
    {
        foreach (ShopItem item in items)
        {
            ShopItemView spawnedItem = _shopItemViewFactory.Get(item, _itemParent);

            spawnedItem.OnClick += OnItemViewClick;
            
            spawnedItem.Unselect();
            spawnedItem.UnHighlight();
            
            _shopItems.Add(spawnedItem);
        }
    }

    private void OnItemViewClick(ShopItemView shopItemView)
    {
        throw new NotImplementedException();
    }

    private void Clear()
    {
        foreach (ShopItemView item in _shopItems)
        {
            item.OnClick -= OnItemViewClick;
            Destroy(item.gameObject);
        }
        
        _shopItems.Clear();
    }
}
