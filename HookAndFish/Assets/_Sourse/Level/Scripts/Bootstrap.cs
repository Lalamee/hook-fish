using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;
public class Bootstrap : MonoBehaviour
{
    [SerializeField] private LevelLoader _levelLoader;
    
    void Update()
    {
        if (YG2.isSDKEnabled)
        {
            if (YG2.isFirstGameSession)
                _levelLoader.OnSceneLoaded(YG2.saves.currentLevel);
            else
                _levelLoader.LoadMenu();
        }
    }
}
