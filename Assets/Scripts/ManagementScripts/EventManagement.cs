using System;
using UnityEngine;

public abstract class EventManagement : MonoSingleton<EventManagement>
{
    public static Action OnSlingshotAimStarted;
    public static Action<Vector2, float> OnSlingshotAiming;
    public static Action<Vector2> OnSlingshotLaunched;
    public static Action<Vector2> OnSlingshotStopped;
    
    //When the time is up
    public static Action OnTimerComplete;
    
    //Event for player getting hit by the bullet
    public static Action<string, Vector3> OnPlayerHit;
    
    // Score changed
    public static Action<int, int> OnScoreChanged;
    
    // UI events
    public static Action SwitchToStartScreen;
    public static Action SwitchToNamePickScreen;
    public static Action SwitchToPauseScreen;
    public static Action SwitchToGameOverScreen;
    public static Action SwitchToGameScreen;

}