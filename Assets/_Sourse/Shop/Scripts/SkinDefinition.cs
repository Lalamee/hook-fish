using UnityEngine;

public enum SkinUnlockType { Level, RewardedAd }

[CreateAssetMenu(fileName = "Skin", menuName = "Shop/Skin")]
public class SkinDefinition : ScriptableObject
{
    [Header("Уникальный ID (должен совпадать с именем для SavesYG)")]
    public string id;

    [Header("Название для UI")]
    public string displayName;

    [Header("Иконка для магазина")]
    public Sprite icon;

    [Header("Тип открытия")]
    public SkinUnlockType unlockType;

    [Header("Если Level")]
    public int requiredLevel = 1;

    [Header("Если RewardedAd")]
    public string rewardedId = "skin_reward_default";

    [Header("Prefab визуала (3D модель)")]
    public GameObject skinPrefab;
}