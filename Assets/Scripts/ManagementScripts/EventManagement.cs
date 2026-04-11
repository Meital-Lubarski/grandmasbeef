using System;
using UnityEngine;

public abstract class EventManagement : MonoSingleton<EventManagement>
{
    public static Action OnSlingshotAimStarted;
    public static Action<Vector2, float> OnSlingshotAiming;
    public static Action<Vector2> OnSlingshotLaunched;
    public static Action<Vector2> OnSlingshotStopped;
    
    
    //Event for player getting hit by the bullet
    public static Action<string, Vector3> OnPlayerHit;
    
    //Events regarding the ammo limitation per round
    public static Action<string> OnPlayerShot;
    public static Action<string, int> OnAmmoChanged;
    public static Action OnProjectileSpawned;
    public static Action OnProjectileResolved;
    
    // Score changed
    public static Action<int, int> OnScoreChanged;
    
    //Time management
    public static System.Action OnTimerComplete;
    public static System.Action<string> OnGameEndedWithWinner;
    
    // UI events
    public static Action SwitchToStartScreen;
    public static Action SwitchToNamePickScreen;
    public static Action SwitchToPauseScreen;
    public static Action SwitchToGameOverScreen;
    public static Action SwitchToGameScreen;

}