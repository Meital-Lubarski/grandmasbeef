using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TMP_Text player1ScoreText;
    [SerializeField] private TMP_Text player2ScoreText;
    [SerializeField] private TMP_Text mapsScoreText;

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

    private void UpdateScoreUI(int player1Hits, int player2Hits)
    {
        // מציג: שם שחקן | ניצחונות במפות: X | פגיעות בסיבוב: Y/6
        player1ScoreText.text = _player1Name + ": Hits: " + player1Hits + "/6";
        player2ScoreText.text = _player2Name + ": Hits: " + player2Hits + "/6";
        mapsScoreText.text = ScoreManager.Instance.Player1MapWins + " - " + ScoreManager.Instance.Player2MapWins;
    }
}