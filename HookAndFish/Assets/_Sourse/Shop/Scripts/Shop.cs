using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private ShopContent _contentItems;

    [SerializeField] private ShopCategoryButton _characterSkinsButton;
    
    [SerializeField] private ShopPanel _shopPanel;


    private void OnEnable()
    {
        _characterSkinsButton.Click += OnCharacterSkinsButtonClick;
    }

    private void OnDisable()
    {
        _characterSkinsButton.Click -= OnCharacterSkinsButtonClick;
    }

    private void OnCharacterSkinsButtonClick()
    {
        _characterSkinsButton.Select();
        _shopPanel.Show(_contentItems.CharacterSkinItem.Cast<ShopItem>());
    }
}
