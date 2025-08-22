using System.Collections.Generic;
using IJunior.TypedScenes;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;
using Random = UnityEngine.Random;

public class LevelLoader : MonoBehaviour, ISceneLoadHandler<int>
{
    public void LoadLevel()
    {
        SceneManager.LoadScene(YG2.saves.currentLevel);
    }

    public void LoadMenu()
    {
        YG2.SaveProgress();
        YG2.InterstitialAdvShow();
        SceneManager.LoadScene(1);
    }

    public void RestartThisLevel()
    {
        YG2.InterstitialAdvShow();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void OnSceneLoaded(int argument)
    {
        SceneManager.LoadScene(argument);
    }

    public void LoadNextLevel()
    {
        int firstPlayableIndex = 2;
        int lastPlayableIndexPlan = 21;
        int historyWindowSize = 3;

        int lastBuildIndex = SceneManager.sceneCountInBuildSettings - 1;
        int minPlayableIndex = Mathf.Clamp(firstPlayableIndex, 0, lastBuildIndex);
        int maxPlayableIndex = Mathf.Min(lastPlayableIndexPlan, lastBuildIndex);

        if (YG2.saves.recentLevels == null)
            YG2.saves.recentLevels = new List<int>(historyWindowSize);

        int currentBuildIndex = SceneManager.GetActiveScene().buildIndex;
        List<int> history = YG2.saves.recentLevels;

        for (int i = history.Count - 1; i >= 0; i--)
        {
            int idx = history[i];
            if (idx < minPlayableIndex || idx > maxPlayableIndex)
                history.RemoveAt(i);
        }

        var excluded = new List<int>();
        if (currentBuildIndex >= minPlayableIndex && currentBuildIndex <= maxPlayableIndex)
            if (!excluded.Contains(currentBuildIndex)) excluded.Add(currentBuildIndex);

        for (int i = 0; i < history.Count && i < historyWindowSize; i++)
        {
            int idx = history[i];
            if (!excluded.Contains(idx)) excluded.Add(idx);
        }

        var candidates = new List<int>();
        for (int i = minPlayableIndex; i <= maxPlayableIndex; i++)
            if (!excluded.Contains(i))
                candidates.Add(i);

        if (candidates.Count == 0)
        {
            for (int i = minPlayableIndex; i <= maxPlayableIndex; i++)
                if (i != currentBuildIndex)
                    candidates.Add(i);

            if (candidates.Count == 0)
                candidates.Add(Mathf.Clamp(currentBuildIndex, minPlayableIndex, maxPlayableIndex));
        }

        int nextBuildIndex = candidates[Random.Range(0, candidates.Count)];

        YG2.saves.currentLevel = nextBuildIndex;

        if (currentBuildIndex >= minPlayableIndex && currentBuildIndex <= maxPlayableIndex)
        {
            history.Insert(0, currentBuildIndex);

            for (int i = 0; i < history.Count; i++)
                for (int j = history.Count - 1; j > i; j--)
                    if (history[j] == history[i])
                        history.RemoveAt(j);

            if (history.Count > historyWindowSize)
                history.RemoveRange(historyWindowSize, history.Count - historyWindowSize);
        }

        YG2.SaveProgress();
        SceneManager.LoadScene(nextBuildIndex);
    }
}
