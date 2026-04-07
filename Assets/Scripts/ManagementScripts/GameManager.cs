using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    private void Awake()
    {
        EventManagement.MoveToOpenScreen?.Invoke();
    }

    public void RestartCurrentScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    public void StartGame()
    {
        EventManagement.OnGameStarted?.Invoke();
    }

    public static void GameOver()
    {
        EventManagement.OnGameOver?.Invoke();
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