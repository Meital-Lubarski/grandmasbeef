using System.Collections;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Round Win Settings")]
    [SerializeField] private GameObject roundWinPanel;
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

    private IEnumerator ShowRoundWinAndContinue(string winnerId)
    {
        DisableShooting();

        string winnerName = PlayerPrefs.GetString(winnerId + "Name", winnerId);
        roundWinnerText.text = winnerName + " has won the round!";
        roundWinPanel.SetActive(true);

        yield return new WaitForSecondsRealtime(delayBeforeNextRound);

        roundWinPanel.SetActive(false);

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

    public void OnStartScreenPlayPressed()
    {
        EventManagement.SwitchToNamePickScreen?.Invoke();
    }

    public void OnNamePickStartBattlePressed()
    {
        GameManager.StartGameImmediately = true;
        GameManager.Instance.RestartCurrentScene();
    }

    public void OnPausePressed()
    {
        EventManagement.SwitchToPauseScreen?.Invoke();
        DisableShooting();
    }

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

    public void OnRestartPressed()
    {
        GameManager.Instance.RestartCurrentScene();
    }

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