using UnityEngine;

public class LevelFinisher : MonoBehaviour
{
    private GoodEnd _goodEnd;
    private BadEnd _badEnd;

    public void Initialize(GoodEnd goodEnd, BadEnd badEnd)
    {
        _goodEnd = goodEnd;
        _badEnd = badEnd;

        _goodEnd?.TurnOffObject();
        _badEnd?.TurnOffObject();
    }

    public void BadEnd()
    {
        Time.timeScale = 0;
        _badEnd.TurnOnObject();
    }

    public void GoodEnd() 
    {
        Time.timeScale = 0;
        _goodEnd.TurnOnObject();
    }
}
