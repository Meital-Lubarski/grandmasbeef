using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TMP_Text player1ScoreText;
    [SerializeField] private TMP_Text player2ScoreText;

    private string _player1Name;
    private string _player2Name;
    
    private void Start()
    {
        _player1Name = PlayerPrefs.GetString("Player1Name", "Player 1");
        _player2Name = PlayerPrefs.GetString("Player2Name", "Player 2");
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

    private void UpdateScoreUI(int player1Score, int player2Score)
    {
        player1ScoreText.text = $"{_player1Name} Score: {player1Score}";
        player2ScoreText.text = $"{_player2Name} Score: {player2Score}";
    }
}