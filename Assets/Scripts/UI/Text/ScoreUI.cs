using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TMP_Text player1ScoreText;
    [SerializeField] private TMP_Text player2ScoreText;
    
    private void Start()
    {
        UpdateScoreUI(0, 0);
    }
    
    private void OnEnable()
    {
        EventManagement.OnScoreChanged += UpdateScoreUI;
    }

    private void OnDisable()
    {
        EventManagement.OnScoreChanged -= UpdateScoreUI;
    }

    private void UpdateScoreUI(int player1Hits, int player2Hits)
    {
        string player1Name = PlayerPrefs.GetString("Player1Name", "Player 1");
        string player2Name = PlayerPrefs.GetString("Player2Name", "Player 2");

        int player1LivesLeft = 6 - player1Hits;
        int player2LivesLeft = 6 - player2Hits;
        
        player1ScoreText.text = player1Name + ": Lives: " + player2LivesLeft;
        player2ScoreText.text = player2Name + ": Lives: " + player1LivesLeft;
    }
}