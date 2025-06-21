using UnityEngine;

public class LevelFinisher : MonoBehaviour
{
    private GoodEnd _goodEnd;
    private BadEnd _badEnd;

    private void Awake()
    {
        _goodEnd = FindObjectOfType<GoodEnd>();
        _badEnd = FindObjectOfType<BadEnd>();
        _goodEnd.TurnOffObject();
        _badEnd.TurnOffObject();
    }

    public void BadEnd()
    {
        _badEnd.TurnOnObject();
        End();
    }

    public void GoodEnd() 
    {
        _goodEnd.TurnOnObject();
        End();
    }

    private void End()
    {
        Time.timeScale = 0;
    }
}
