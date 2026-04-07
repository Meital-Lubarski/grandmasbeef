using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
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