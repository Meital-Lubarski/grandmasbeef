using UnityEngine;

public class UIManager : MonoBehaviour
{
    private void OnEnable()
    {
        EventManagement.OnRoundComplete += HandleRoundComplete;
        EventManagement.OnGameEndedWithWinner += HandleGameEnded;
    }

    private void OnDisable()
    {
        EventManagement.OnRoundComplete -= HandleRoundComplete;
        EventManagement.OnGameEndedWithWinner -= HandleGameEnded;
    }

    private void HandleRoundComplete(string winnerId)
    {
        /*TODO: add a screen with who won the game....
        Right now going straight into the next round*/
        MapManager.Instance.AdvanceToNextRound();
        EventManagement.SwitchToGameScreen?.Invoke();
    }

    private void HandleGameEnded(string winnerId)
    {
        EventManagement.SwitchToGameOverScreen?.Invoke();
    }

    // --- מסך התחלה ---
    public void OnStartScreenPlayPressed()
    {
        EventManagement.SwitchToNamePickScreen?.Invoke();
    }

    // --- מסך בחירת שמות ---
    public void OnNamePickStartBattlePressed()
    {
        EventManagement.ResetPoints?.Invoke();
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
        //TODO: change to a soft restart instead of resetting the entire scene
        GameManager.Instance.RestartCurrentScene();
    }

    // --- כללי ---
    public void OnQuitPressed()
    {
        GameManager.QuitGame();
    }
}