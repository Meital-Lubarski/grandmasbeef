using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TMP_Text player1ScoreText;
    [SerializeField] private TMP_Text player2ScoreText;

    private string _player1Name;
    private string _player2Name;
    private ScoreManager _scoreManager;
    
    private void Start()
    {
        _player1Name = PlayerPrefs.GetString("Player1Name", "Player 1");
        _player2Name = PlayerPrefs.GetString("Player2Name", "Player 2");
        _scoreManager = FindObjectOfType<ScoreManager>();
        
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
        if (_scoreManager == null) return;

        // מציג: שם שחקן | ניצחונות במפות: X | פגיעות בסיבוב: Y/6
        player1ScoreText.text = $"{_player1Name}\nMaps Won: {_scoreManager.Player1MapWins}/2\nHits: {player1Hits}/6";
        player2ScoreText.text = $"{_player2Name}\nMaps Won: {_scoreManager.Player2MapWins}/2\nHits: {player2Hits}/6";
    }
}