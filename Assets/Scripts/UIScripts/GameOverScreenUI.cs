using UnityEngine;

public class GameOverScreenUI : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;

    private void OnEnable()
    {
        EventManagement.OnGameOver += HandleGameOver;
    }

    private void OnDisable()
    {
        EventManagement.OnGameOver -= HandleGameOver;
    }

    private void Start()
    {
        gameOverPanel.SetActive(false);
    }

    private void HandleGameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void OnRestartPressed()
    {
        Time.timeScale = 1f;
        GameManager.Instance.RestartCurrentScene();
    }

    public void OnQuitPressed()
    {
        GameManager.QuitGame();
    }
}