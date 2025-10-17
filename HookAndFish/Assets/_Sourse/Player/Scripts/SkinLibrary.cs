using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "SkinLibrary", menuName = "Shop/SkinLibrary")]
public class SkinLibrary : ScriptableObject
{
    public SkinDefinition[] skins;

    public SkinDefinition Find(string id) =>
        skins.FirstOrDefault(skin => skin.id == id);
}