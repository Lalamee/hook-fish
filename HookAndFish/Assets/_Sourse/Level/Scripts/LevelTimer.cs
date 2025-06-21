using UnityEngine;
using TMPro;

public class LevelTimer : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;

    private LevelFinisher _levelFinisher;
    private int _levelMinutes = 0; 
    private int _levelSeconds = 30; 
    private float _currentTime = 0f;

    void Start()
    {
        _levelFinisher = FindObjectOfType<LevelFinisher>();

        StartLevelTimer();
    }

    void Update()
    {
        _currentTime -= Time.deltaTime;
        UpdateTimerUI();

        if (_currentTime <= 0f)
            LevelEnd();
    }

    void StartLevelTimer()
    {
        _currentTime = _levelMinutes * 60 + _levelSeconds;
        UpdateTimerUI();
    }

    void UpdateTimerUI()
    {
        if(_currentTime <= 10f)
            timerText.color = Color.red;
        else
            timerText.color = Color.white;  
        
        int minutes = Mathf.FloorToInt(_currentTime / 60);
        int seconds = Mathf.FloorToInt(_currentTime % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void LevelEnd()
    {
        _levelFinisher.BadEnd();
    }
}