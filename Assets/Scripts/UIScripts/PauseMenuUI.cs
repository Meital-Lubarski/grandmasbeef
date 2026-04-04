using UnityEngine;

public class PauseMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;

    private bool _isPaused = false;

    private void Start()
    {
        pausePanel.SetActive(false);
    }

    public void OnPause()
    {
        TogglePause();
    }

    private void TogglePause()
    {
        _isPaused = !_isPaused;

        pausePanel.SetActive(_isPaused);
        Time.timeScale = _isPaused ? 0f : 1f;
    }

    public void OnResumePressed()
    {
        _isPaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void OnRestartPressed()
    {
        Time.timeScale = 1f;
        GameManager.Instance.RestartCurrentScene();
    }

    public void OnExitPressed()
    {
        GameManager.QuitGame();
    }
}