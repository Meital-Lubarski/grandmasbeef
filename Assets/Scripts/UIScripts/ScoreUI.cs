using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TMP_Text player1ScoreText;
    [SerializeField] private TMP_Text player2ScoreText;

    private int _player1Score;
    private int _player2Score;

    private void Start()
    {
        UpdateScoreUI();
    }

    public void AddScoreToPlayer1(int amount)
    {
        _player1Score += amount;
        UpdateScoreUI();
    }

    public void AddScoreToPlayer2(int amount)
    {
        _player2Score += amount;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        player1ScoreText.text = "Player 1 Score: " + _player1Score;
        player2ScoreText.text = "Player 2 Score: " + _player2Score;
    }
}