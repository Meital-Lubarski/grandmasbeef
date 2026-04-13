using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private CountdownTimer countdownTimer;

    private void Update()
    {
        float time = countdownTimer.GetTimeRemaining();
        UpdateTimerDisplay(time);
    }

    private void UpdateTimerDisplay(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}