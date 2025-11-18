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
