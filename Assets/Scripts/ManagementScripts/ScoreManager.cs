using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private const string Player1Id = "Player1";
    private const string Player2Id = "Player2";
    private const string TieId = "Tie";

    private int _player1Score;
    private int _player2Score;

    public int Player1Score => _player1Score;
    public int Player2Score => _player2Score;

    private void OnEnable()
    {
        EventManagement.OnPlayerHit += HandlePlayerHit;
        EventManagement.OnTimerComplete += HandleTimerComplete;
    }

    private void OnDisable()
    {
        EventManagement.OnPlayerHit -= HandlePlayerHit;
        EventManagement.OnTimerComplete -= HandleTimerComplete;
    }

    private void HandlePlayerHit(string hitPlayerId, Vector3 hitDirection)
    {
        if (hitPlayerId == Player1Id)
        {
            _player2Score++;
        }
        else if (hitPlayerId == Player2Id)
        {
            _player1Score++;
        }

        NotifyScoreChanged();
    }

    private void HandleTimerComplete()
    {
        string winnerId = GetWinnerId();
        EventManagement.OnGameEndedWithWinner?.Invoke(winnerId);
    }

    private string GetWinnerId()
    {
        if (_player1Score > _player2Score)
        {
            return Player1Id;
        }

        if (_player2Score > _player1Score)
        {
            return Player2Id;
        }

        return TieId;
    }

    private void NotifyScoreChanged()
    {
        EventManagement.OnScoreChanged?.Invoke(_player1Score, _player2Score);
    }

    public void ResetScores()
    {
        _player1Score = 0;
        _player2Score = 0;
        NotifyScoreChanged();
    }
}