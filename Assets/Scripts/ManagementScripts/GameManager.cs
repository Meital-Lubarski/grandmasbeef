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

    public void HandleFinalGameEnd()
    {
        string winnerMessage;

        if (scoreManager.Player1Score > scoreManager.Player2Score)
        {
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

        EventManagement.OnGameEndedWithWinner?.Invoke(winnerMessage);
        EventManagement.SwitchToGameOverScreen?.Invoke();
    }

    public void RestartCurrentScene()
    {
        Time.timeScale = 1f;
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