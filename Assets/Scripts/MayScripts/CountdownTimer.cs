using UnityEngine;

public class CountdownTimer : MonoBehaviour
{
    [Header("Timer Settings")]
    [Tooltip("Starting time in seconds (120 seconds = 2 minutes)")]
    [SerializeField] private float roundDuration = 120f;

    private float _timeRemaining;
    private bool _timerIsRunning = false;

    private void OnEnable()
    {
        EventManagement.SwitchToGameScreen += ResetAndStartTimer;
        EventManagement.SwitchToPauseScreen += StopTimer;
        EventManagement.SwitchToStartScreen += StopTimer;
        EventManagement.SwitchToNamePickScreen += StopTimer;
        EventManagement.SwitchToGameOverScreen += StopTimer;
    }

    private void OnDisable()
    {
        EventManagement.SwitchToGameScreen -= ResetAndStartTimer;
        EventManagement.SwitchToPauseScreen -= StopTimer;
        EventManagement.SwitchToStartScreen -= StopTimer;
        EventManagement.SwitchToNamePickScreen -= StopTimer;
        EventManagement.SwitchToGameOverScreen -= StopTimer;
    }

    private void Start()
    {
        _timeRemaining = roundDuration;
    }

    private void ResetAndStartTimer()
    {
        _timeRemaining = roundDuration;
        _timerIsRunning = true;
    }

    private void StopTimer()
    {
        _timerIsRunning = false;
    }

    private void Update()
    {
        if (!_timerIsRunning)
        {
            return;
        }

        if (_timeRemaining > 0f)
        {
            _timeRemaining -= Time.deltaTime;
        }
        else
        {
            _timeRemaining = 0f;
            _timerIsRunning = false;
            EventManagement.OnTimerComplete?.Invoke();
        }
    }

    public float GetTimeRemaining()
    {
        return _timeRemaining;
    }
}