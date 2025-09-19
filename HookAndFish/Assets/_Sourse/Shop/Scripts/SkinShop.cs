using System;
using System.Linq;
using UnityEngine;
using YG;

public class SkinShop : MonoBehaviour
{
    [Header("Список скинов (SO)")]
    [SerializeField] private SkinDefinition[] skins;

    public event Action OnSkinsChanged;   // <<< событие для UI

    private SkinDefinition Find(string id) => skins.FirstOrDefault(s => s.id == id);

    // Открыть скин по уровню (и сразу выбрать)
    public void TryUnlockLevelSkin(string skinId)
    {
        var def = Find(skinId);
        if (def == null || def.unlockType != SkinUnlockType.Level) return;
        if (YG2.saves.IsSkinUnlocked(skinId)) { TrySelectSkin(skinId); return; }

        if (YG2.saves.playerLevel >= def.requiredLevel)
        {
            UnlockAndSelect(skinId);
            NotifyChanged(); // <<<
        }
        else
        {
            Debug.Log($"Нужен уровень {def.requiredLevel}, сейчас {YG2.saves.playerLevel}");
        }
    }

    // Открыть скин за рекламу (и сразу выбрать)
    public void TryUnlockRewardedSkin(string skinId)
    {
        var def = Find(skinId);
        if (def == null || def.unlockType != SkinUnlockType.RewardedAd) return;

        if (YG2.saves.IsSkinUnlocked(skinId)) { TrySelectSkin(skinId); return; }

        string rewardId = string.IsNullOrEmpty(def.rewardedId) ? def.id : def.rewardedId;

        // коллбэк YG2 — после вознаграждения сразу оповещаем UI
        YG2.RewardedAdvShow(rewardId, () =>
        {
            UnlockAndSelect(skinId);
            NotifyChanged(); // <<< ключевой момент
        });
    }

    // Просто выбрать уже открытый
    public void TrySelectSkin(string skinId)
    {
        if (!YG2.saves.IsSkinUnlocked(skinId)) return;

        YG2.saves.selectedSkinId = skinId;
        YG2.SaveProgress();
        NotifyChanged(); // <<<
    }

    private void UnlockAndSelect(string skinId)
    {
        YG2.saves.AddUnlockedSkin(skinId);
        YG2.saves.selectedSkinId = skinId;
        YG2.SaveProgress();
    }

    private void NotifyChanged() => OnSkinsChanged?.Invoke();
}
