using IJunior.TypedScenes;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class LevelLoader : MonoBehaviour, ISceneLoadHandler<int>
{
    public void LoadLevel()
    {
        SceneManager.LoadScene(YG2.saves.currentLevel);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void RestartThisLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void OnSceneLoaded(int argument)
    {
        SceneManager.LoadScene(argument);
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(YG2.saves.currentLevel + 1);
    }
}