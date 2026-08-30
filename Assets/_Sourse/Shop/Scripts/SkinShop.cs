using System;
using System.Linq;
using UnityEngine;
using YG;

public class SkinShop : MonoBehaviour
{
    [SerializeField] private SkinLibrary skinLibrary;
    public event Action OnSkinsChanged;

    public string CurrentSelectedId => YG2.saves?.selectedSkinId;

    public SkinDefinition[] GetAllSkins() =>
        skinLibrary != null && skinLibrary.skins != null
            ? skinLibrary.skins
            : Array.Empty<SkinDefinition>();

    public SkinDefinition[] GetSkinsSorted()
    {
        var all = GetAllSkins();

        int Priority(SkinDefinition s) => s.unlockType switch
        {
            SkinUnlockType.Level => 0,
            SkinUnlockType.RewardedAd => 1,
            _ => 2
        };

        return all
            .OrderBy(Priority)
            .ThenBy(s => s.unlockType == SkinUnlockType.Level ? s.requiredLevel : int.MaxValue)
            .ThenBy(s => s.displayName)
            .ToArray();
    }

    public void TryUnlockLevelSkin(string skinId)
    {
        var def = Find(skinId);
        if (def == null || def.unlockType != SkinUnlockType.Level) return;

        if (YG2.saves.IsSkinUnlocked(skinId))
        {
            TrySelectSkin(skinId);
            return;
        }

        if (YG2.saves.playerLevel >= def.requiredLevel)
        {
            UnlockAndSelect(skinId);
            NotifyChanged();
        }
        else
        {
            Debug.Log($"[SkinShop] Нужен уровень {def.requiredLevel}, сейчас {YG2.saves.playerLevel}");
        }
    }

    public void TryUnlockRewardedSkin(string skinId)
    {
        var def = Find(skinId);
        if (def == null || def.unlockType != SkinUnlockType.RewardedAd) return;

        if (YG2.saves.IsSkinUnlocked(skinId))
        {
            TrySelectSkin(skinId);
            return;
        }

        string rewardId = string.IsNullOrEmpty(def.rewardedId) ? def.id : def.rewardedId;

        YG2.RewardedAdvShow(rewardId, () =>
        {
            UnlockAndSelect(skinId);
            NotifyChanged();
        });
    }

    public void TrySelectSkin(string skinId)
    {
        if (!YG2.saves.IsSkinUnlocked(skinId)) return;

        YG2.saves.selectedSkinId = skinId;
        YG2.SaveProgress();
        NotifyChanged();
    }

    private SkinDefinition Find(string id)
    {
        if (skinLibrary == null)
        {
            Debug.LogWarning("[SkinShop] SkinLibrary не назначен в инспекторе.");
            return null;
        }
        return skinLibrary.Find(id);
    }

    private void UnlockAndSelect(string skinId)
    {
        YG2.saves.AddUnlockedSkin(skinId);
        YG2.saves.selectedSkinId = skinId;
        YG2.SaveProgress();
    }

    private void NotifyChanged() => OnSkinsChanged?.Invoke();

#if UNITY_EDITOR
    [ContextMenu("DEBUG: Unlock All Skins")]
    private void DebugUnlockAll()
    {
        foreach (var s in GetAllSkins())
        {
            if (s == null) continue;
            YG2.saves.AddUnlockedSkin(s.id);
        }
        YG2.SaveProgress();
        NotifyChanged();
        Debug.Log("[SkinShop] Все скины разблокированы (DEBUG).");
    }
#endif
}
