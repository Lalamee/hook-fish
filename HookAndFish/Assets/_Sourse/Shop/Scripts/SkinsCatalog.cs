using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Shop/Skins Catalog", fileName = "SkinsCatalog")]
public class SkinsCatalog : ScriptableObject
{
    [SerializeField] private List<PlayerSkinItem> _playerSkinItems;

    private void OnValidate()
    {
        var playerSkinsDuplicates = _playerSkinItems.GroupBy(item => item.Id).Where(group => group.Count() > 1);
        
        if (playerSkinsDuplicates.Count() > 0)
            throw new InvalidOperationException("SkinsCatalog contains duplicate player skins." + nameof(_playerSkinItems));
    }
}