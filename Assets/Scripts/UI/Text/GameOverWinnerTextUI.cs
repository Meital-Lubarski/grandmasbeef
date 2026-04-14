using TMPro;
using UnityEngine;

public class GameOverWinnerTextUI : MonoBehaviour
{
    [SerializeField] private TMP_Text winnerText;
    
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

        string player1Name = PlayerPrefs.GetString("Player1Name", "Player 1");
        string player2Name = PlayerPrefs.GetString("Player2Name", "Player 2");
        
        if (winnerId == "Player1")
        {
            winnerText.text = player1Name + " Wins!";
        }
        else if (winnerId == "Player2")
        {
            winnerText.text = player2Name + " Wins!";
        }
        else
        {
            winnerText.text = "It's a Tie!";
        }
    }
}