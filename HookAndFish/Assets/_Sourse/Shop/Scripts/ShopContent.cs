using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopContent", menuName = "Shop/ShopContent")]
public class ShopContent : ScriptableObject
{
    [SerializeField] private List<CharacterSkinItem> _characterSkinItem;
    
    public IEnumerable<CharacterSkinItem> CharacterSkinItem => _characterSkinItem;

    private void OnValidate()
    {
        var characterSkinsDuplicates = _characterSkinItem.GroupBy(item => item.SkinType).Where(array => array.Count() > 1);
        
        if(characterSkinsDuplicates.Count() > 0)
            throw new InvalidOperationException(nameof(_characterSkinItem));
    }
}
