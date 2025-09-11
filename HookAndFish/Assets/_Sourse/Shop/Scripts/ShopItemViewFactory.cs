using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemViewFactory : ScriptableObject
{
    [SerializeField] private ShopItemView _playerSkinItemPrefab;

    public ShopItemView Get(PlayerSkinItem playerSkinItem, Transform parent)
    {
        ShopItemView instance;
        
        instance = Instantiate(_playerSkinItemPrefab, parent);
        instance.Initialize(playerSkinItem);

        return instance;
    }
}
