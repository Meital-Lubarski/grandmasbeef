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
}