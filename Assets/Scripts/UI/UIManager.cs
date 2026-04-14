using System.Collections;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Round Win Settings")]
    [SerializeField] private GameObject roundWinPanel; //panel for round winner
    [SerializeField] private TMP_Text roundWinnerText;
    [SerializeField] private float delayBeforeNextRound = 2f;
    private void Start()
    {
        DisableShooting(); 
    }
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
        StartCoroutine(ShowRoundWinAndContinue(winnerId));
    }

    //Coroutine for resetting players location and saying who is the winner in each round
    private IEnumerator ShowRoundWinAndContinue(string winnerId)
    {
        DisableShooting();
        string winnerName = PlayerPrefs.GetString(winnerId + "Name", winnerId);
        
        //turning on the winner canvas & text
        roundWinnerText.text = winnerName + " has won the round!";
        roundWinPanel.SetActive(true);

        yield return new WaitForSecondsRealtime(delayBeforeNextRound);
        
        roundWinPanel.SetActive(false);
        
        //Going into the next map and resetting the game
        MapManager.Instance.AdvanceToNextRound();
        
        EnableShooting();
        EventManagement.OnResetPositions?.Invoke(); 
        
        EventManagement.SwitchToGameScreen?.Invoke();
    }

    private void HandleGameEnded(string winnerId)
    {
        DisableShooting();
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
        EnableShooting();
    }

    // --- משחק (HUD) ---
    public void OnPausePressed()
    {
        EventManagement.SwitchToPauseScreen?.Invoke();
        DisableShooting();
    }

    // --- תפריט עצירה ---
    public void OnResumePressed()
    {
        EventManagement.SwitchToGameScreen?.Invoke();
        EnableShooting();
    }

    public void OnExitToStartPressed()
    {
        EventManagement.SwitchToStartScreen?.Invoke();
        DisableShooting();
    }

    // --- מסך Game Over ---
    public void OnRestartPressed()
    {
        GameManager.Instance.RestartCurrentScene();
        //Can be changed into SOFT restart: (recommended, not working good yet)
        //OnNamePickStartBattlePressed();
    }

    // --- כללי ---
    public void OnQuitPressed()
    {
        GameManager.QuitGame();
    }

    private void EnableShooting()
    {
        EventManagement.SetShootingEnabled?.Invoke(true);
    }
    private void DisableShooting()
    {
        EventManagement.SetShootingEnabled?.Invoke(false);
    }
}