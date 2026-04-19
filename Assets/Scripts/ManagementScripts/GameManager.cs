using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    public static bool StartGameImmediately = false;

    private void Start()
    {
        if (StartGameImmediately)
        {
            StartGameImmediately = false;
            EventManagement.SwitchToGameScreen?.Invoke();
            EventManagement.SetShootingEnabled?.Invoke(true);
        }
        else
        {
            EventManagement.SwitchToStartScreen?.Invoke();
        }
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