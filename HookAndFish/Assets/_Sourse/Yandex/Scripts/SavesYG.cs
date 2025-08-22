using System.Collections.Generic;

namespace YG
{
    public partial class SavesYG
    {
		public int playerLevel = 1;
        public int currentLevel = 2;
        public float volume = 0.5f;
        public List<int> recentLevels = new List<int>(3);
    }
}
