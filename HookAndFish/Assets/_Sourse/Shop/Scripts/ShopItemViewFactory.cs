using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopItemViewFactory", menuName = "Shop/ShopItemViewFactory")]
public class ShopItemViewFactory : ScriptableObject
{
    [SerializeField] private ShopItemView _characterSkinItemPrefab;
    
    public ShopItemView Get(ShopItem shopItem, Transform parent)
    {
        ShopItemView instance = Instantiate(_characterSkinItemPrefab, parent);
        
        instance.Initialize(shopItem);
        return instance;
    }
}
