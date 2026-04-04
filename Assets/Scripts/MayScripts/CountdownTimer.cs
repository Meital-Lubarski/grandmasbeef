using UnityEngine;

public class CountdownTimer : MonoBehaviour
{
    [Header("Timer Settings")]
    [Tooltip("Starting time in seconds (120 seconds = 2 minutes)")]
    [SerializeField] private float timeRemaining = 120f;
    [SerializeField] private bool timerIsRunning = false;
    private void Start()
    {
        // Start the timer as soon as the object loads
        timerIsRunning = true;
    }

    private void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                // Subtract the time since the last frame
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                // Time is up! 
                timeRemaining = 0;
                timerIsRunning = false;
                
                // Trigger whatever events are hooked up in the Inspector
                EventManagement.OnTimerComplete.Invoke();
            }
        }
    }
    public float GetTimeRemaining()
    {
        return timeRemaining;
    }
}