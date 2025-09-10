using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSkinItem", menuName = "Shop/PlayerSkinItem")]
public class PlayerSkinItem : ScriptableObject
{
    [field: SerializeField] public PlayerSkins SkinType { get; private set; }
    [field: SerializeField] public GameObject SkinObject { get; private set; }
    [field: SerializeField] public Sprite Image { get; private set; }
    [field: SerializeField, Range(0,150)] public int Price { get; private set; }
}
