using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TMP_Text player1ScoreText;
    [SerializeField] private TMP_Text player2ScoreText;
    [SerializeField] private TMP_Text mapsScoreText;
    
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

        player1ScoreText.text = player1Name + ": Hits: " + player1Hits + "/6";
        player2ScoreText.text = player2Name + ": Hits: " + player2Hits + "/6";
        
        if (ScoreManager.Instance == null)
            return;
        
        mapsScoreText.text = ScoreManager.Instance.Player1MapWins + " - " + ScoreManager.Instance.Player2MapWins;
    }
}