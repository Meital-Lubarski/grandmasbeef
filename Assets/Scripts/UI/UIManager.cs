using UnityEngine;

public class UIManager : MonoBehaviour
{
    private void OnEnable()
    {
        EventManagement.OnTimerComplete += OnTimerComplete;
    }

    private void OnDisable()
    {
        EventManagement.OnTimerComplete -= OnTimerComplete;
    }

    private void OnTimerComplete()
    {
        if (MapManager.Instance.HasMoreRounds())
        {
            MapManager.Instance.AdvanceToNextRound();
            EventManagement.SwitchToGameScreen?.Invoke();
        }
        else
        {
            GameManager.Instance.HandleFinalGameEnd();
        }
    }

    // --- מסך התחלה ---
    public void OnStartScreenPlayPressed()
    {
        EventManagement.SwitchToNamePickScreen?.Invoke();
    }

    // --- מסך בחירת שמות ---
    public void OnNamePickStartBattlePressed()
    {
        MapManager.Instance.StartNewMatch();
        EventManagement.SwitchToGameScreen?.Invoke();
    }

    // --- משחק (HUD) ---
    public void OnPausePressed()
    {
        EventManagement.SwitchToPauseScreen?.Invoke();
    }

    // --- תפריט עצירה ---
    public void OnResumePressed()
    {
        EventManagement.SwitchToGameScreen?.Invoke();
    }

    public void OnExitToStartPressed()
    {
        EventManagement.SwitchToStartScreen?.Invoke();
    }

    // --- מסך Game Over ---
    public void OnRestartPressed()
    {
        GameManager.Instance.RestartCurrentScene();
    }

    // --- כללי ---
    public void OnQuitPressed()
    {
        GameManager.QuitGame();
    }
}