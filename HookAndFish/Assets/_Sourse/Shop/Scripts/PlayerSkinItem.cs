using UnityEngine;

public enum SkinUnlockType { Rewarded, ByLevel }

[CreateAssetMenu(menuName = "Shop/Player Skin", fileName = "PlayerSkin_")]
public class PlayerSkinItem : ScriptableObject
{
    [SerializeField] private PlayerSkins id;
    [SerializeField] private string displayName;
    [SerializeField] private Sprite icon;
    [SerializeField] public GameObject playerPrefab;

    [Header("Unlock")]
    [SerializeField] private SkinUnlockType unlockType = SkinUnlockType.Rewarded;
    [SerializeField, Range(0,100000)] private int requiredLevel = 0; 

    public PlayerSkins Id => id;
    public string DisplayName => displayName;
    public Sprite Icon => icon;
    public GameObject PlayerPrefab => playerPrefab;
    public SkinUnlockType UnlockType => unlockType;
    public int RequiredLevel => requiredLevel;
}