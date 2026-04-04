using UnityEngine;
using UnityEngine.Android;

public class ScoreManager : MonoBehaviour
{
    private int _player1Score;
    private int _player2Score;
    private void OnEnable()
    {
        EventManagement.OnPlayerHit += HandlePlayerHit;
    }

    private void OnDisable()
    {
        EventManagement.OnPlayerHit -= HandlePlayerHit;
    }

    private void HandlePlayerHit(string hitPlayerId, Vector3 hitDirection)
    {
        if (hitPlayerId == "Player1")
        {
            _player2Score++;
        }
        else if (hitPlayerId == "Player2")
        {
            _player1Score++;
        }
        NotifyScoreChanged();
    }
    
    private void NotifyScoreChanged()
    {
        EventManagement.OnScoreChanged?.Invoke(_player1Score, _player2Score);
    }
}
