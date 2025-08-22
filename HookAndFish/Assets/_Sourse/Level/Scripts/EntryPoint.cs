using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class EntryPoint : MonoBehaviour
{
    void Awake()
    {
        YG2.saves.currentLevel = SceneManager.GetActiveScene().buildIndex;        
    }
}
