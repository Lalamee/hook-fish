using UnityEngine;
using YG;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private LevelLoader _levelLoader;
    private bool _started;

    private void OnEnable()
    {
        YG2.onGetSDKData += OnSdkReady;
    }

    private void OnDisable()
    {
        YG2.onGetSDKData -= OnSdkReady;
    }

    private void Start()
    {
        if (YG2.isSDKEnabled) OnSdkReady();
    }

    private void OnSdkReady()
    {
        if (_started) return;
        _started = true;

        if (YG2.isFirstGameSession)
        {
            _levelLoader.OnSceneLoaded(YG2.saves.currentLevel);
        }
        else
        {
            _levelLoader.LoadMenu();
        }
    }
}