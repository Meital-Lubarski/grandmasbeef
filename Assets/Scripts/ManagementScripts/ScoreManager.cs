using UnityEngine;

public class ScoreManager : MonoSingleton<ScoreManager>
{
    [Header("Game Settings")]
    public int hitsToWinRound = 6;
    public int roundsToWinGame = 2;
    
    private const string Player1Id = "Player1";
    private const string Player2Id = "Player2";

    private int _player1RoundHits;
    private int _player2RoundHits;

    public int Player1MapWins { get; private set; }
    public int Player2MapWins { get; private set; }
    private void OnEnable()
    {
        EventManagement.OnPlayerHit += HandlePlayerHit;
        EventManagement.ResetPoints += ResetAllMatchScores;
    }

    private void OnDisable()
    {
        EventManagement.OnPlayerHit -= HandlePlayerHit;
        EventManagement.ResetPoints -= ResetAllMatchScores;
    }

    private void HandlePlayerHit(string hitPlayerId, Vector3 hitDirection)
    {
        if (hitPlayerId == Player1Id)
        {
            _player2RoundHits++;
        }
        else if (hitPlayerId == Player2Id)
        {
            _player1RoundHits++;
        }

        NotifyScoreChanged();
        CheckRoundWinner();
    }

    private void CheckRoundWinner()
    {
        if (_player1RoundHits >= hitsToWinRound)
        {
            HandleRoundEnd(Player1Id);
        }
        else if (_player2RoundHits >= hitsToWinRound)
        {
            HandleRoundEnd(Player2Id);
        }
    }
    
    //Adding win in a map to the current winner
    private void HandleRoundEnd(string roundWinnerId)
    {
        if (roundWinnerId == Player1Id) Player1MapWins++;
        else if (roundWinnerId == Player2Id) Player2MapWins++;
        
        //Reset hits toward the next map
        ResetScores();
        
        //Checks if one of the players won 2 rounds already
        if (Player1MapWins >= roundsToWinGame)
        {
            EventManagement.OnGameEndedWithWinner?.Invoke(Player1Id);
        }
        else if (Player2MapWins >= roundsToWinGame)
        {
            EventManagement.OnGameEndedWithWinner?.Invoke(Player2Id);
        }
        else
        {
            //If no one won invoking going into the next round
            EventManagement.OnRoundComplete?.Invoke(roundWinnerId);
        }
    }
    
    private void NotifyScoreChanged()
    {
        EventManagement.OnScoreChanged?.Invoke(_player1RoundHits, _player2RoundHits);
    }
    private void ResetScores()
    {
        _player1RoundHits = 0;
        _player2RoundHits = 0;
        NotifyScoreChanged();
    }

    //This method is for starting a completely new game 
    private void ResetAllMatchScores()
    {
        _player1RoundHits = 0;
        _player2RoundHits = 0;
        Player1MapWins = 0;
        Player2MapWins = 0;
        NotifyScoreChanged();
    }
}