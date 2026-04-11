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
        Debug.Log("Winner event received: " + winnerId);

        if (winnerText == null)
        {
            Debug.Log("winnerText is null");
            return;
        }

        if (winnerId == "Player1")
        {
            winnerText.text = "Player 1 Wins!";
        }
        else if (winnerId == "Player2")
        {
            winnerText.text = "Player 2 Wins!";
        }
        else
        {
            winnerText.text = "It's a Tie!";
        }
    }
}