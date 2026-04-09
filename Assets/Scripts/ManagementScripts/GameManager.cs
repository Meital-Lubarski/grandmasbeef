using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    [Header("References")]
    [SerializeField] private ScoreManager scoreManager;
    private void Start()
    {
        EventManagement.SwitchToStartScreen?.Invoke();
    }

    private void OnEnable()
    {
        EventManagement.SwitchToStartScreen += PauseTimeScale;
        EventManagement.SwitchToNamePickScreen += PauseTimeScale;
        EventManagement.SwitchToPauseScreen += PauseTimeScale;
        EventManagement.SwitchToGameOverScreen += PauseTimeScale;
        EventManagement.SwitchToGameScreen += ContinueTimeScale;
        EventManagement.OnTimerComplete += HandleGameEnd;
    }

    private void OnDisable()
    {
        EventManagement.SwitchToStartScreen -= PauseTimeScale;
        EventManagement.SwitchToNamePickScreen -= PauseTimeScale;
        EventManagement.SwitchToPauseScreen -= PauseTimeScale;
        EventManagement.SwitchToGameOverScreen -= PauseTimeScale;
        
        EventManagement.SwitchToGameScreen -= ContinueTimeScale;
    }

    private void PauseTimeScale()
    {
        Time.timeScale = 0f;
    }

    private void ContinueTimeScale()
    {
        Time.timeScale = 1f;
    }
    
    //TODO: fits the player's name to the right winner
    private void HandleGameEnd()
    {
        string winnerMessage = "";
        if (scoreManager.Player1Score > scoreManager.Player2Score)
        {
            // אם שמרת שמות ב-PlayerPrefs אפשר לשלוף אותם כאן, למשל:
            // string p1Name = PlayerPrefs.GetString("Player1Name", "Player 1");
            winnerMessage = "Player 1 Wins!";
        }
        else if (scoreManager.Player2Score > scoreManager.Player1Score)
        {
            winnerMessage = "Player 2 Wins!";
        }
        else
        {
            winnerMessage = "It's a Tie!";
        }

        // 1. משדרים את אירוע הניצחון עם הטקסט המתאים (כדי שה-UI יוכל להציג אותו)
        EventManagement.OnGameEndedWithWinner?.Invoke(winnerMessage);
        
        // 2. מעבירים את המשחק למצב Game Over (מה שיעצור את הזמן ויקפיץ את הפאנל)
        EventManagement.SwitchToGameOverScreen?.Invoke();
    }

    public void RestartCurrentScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    public static void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}