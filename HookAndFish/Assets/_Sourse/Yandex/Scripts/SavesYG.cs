using System.Collections.Generic;

namespace YG
{
    public partial class SavesYG
    {
        public int playerLevel = 1;
        public int currentLevel = 1;

        public float musicVolume = 0.5f;
        public float sfxVolume = 0.5f;

        public List<int> recentLevels = new List<int>(3);

        public string selectedSkinId = "";
        public List<string> unlockedSkinIds = new List<string>();

        public bool IsSkinUnlocked(string id)
        {
            if (unlockedSkinIds == null)
                unlockedSkinIds = new List<string>();
            return unlockedSkinIds.Contains(id);
        }

        public void AddUnlockedSkin(string id)
        {
            if (unlockedSkinIds == null)
                unlockedSkinIds = new List<string>();

            if (!unlockedSkinIds.Contains(id))
                unlockedSkinIds.Add(id);
        }
    }
}