using TMPro;
using UnityEngine;

public class GameOverWinnerTextUI : MonoBehaviour
{
    [SerializeField] private TMP_Text winnerText;
    
    private string _player1Name;
    private string _player2Name;
    
    private void Start()
    {
        _player1Name = PlayerPrefs.GetString("Player1Name", "Player 1");
        _player2Name = PlayerPrefs.GetString("Player2Name", "Player 2");
    }
    
    private void OnEnable()
    {
        EventManagement.OnGameEndedWithWinner += UpdateWinnerText;
    }

    private void OnDisable()
    {
        EventManagement.OnGameEndedWithWinner -= UpdateWinnerText;
    }

    private void UpdateWinnerText(string winnerId)
    {
        if (winnerText == null) return;

        if (winnerId == "Player1")
        {
            winnerText.text = $"{_player1Name} Wins!";
        }
        else if (winnerId == "Player2")
        {
            winnerText.text = $"{_player2Name} Wins!";
        }
        else
        {
            winnerText.text = "It's a Tie!";
        }
    }
}