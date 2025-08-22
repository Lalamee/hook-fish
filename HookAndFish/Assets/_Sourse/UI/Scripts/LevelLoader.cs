using System;
using System.Collections.Generic;
using System.Linq;
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
        YG2.InterstitialAdvShow();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void OnSceneLoaded(int argument)
    {
        SceneManager.LoadScene(argument);
    }

    public void LoadNextLevel()
    {
        YG2.saves.currentLevel++;
        YG2.InterstitialAdvShow();
        SceneManager.LoadScene(YG2.saves.currentLevel);
    }

}