using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPanel : MonoBehaviour
{
    private List<ShopItemView> _shopItems = new List<ShopItemView>();
    
    [SerializeField] private Transform _itemParent;

    public void Show(List<ShopItemView> shopItems)
    {
        
    }
}
