using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    public void RestartCurrentScene()
    {
        // Get the currently active scene
        Scene currentScene = SceneManager.GetActiveScene();
        
        // Tell Unity to load it again using its build index
        SceneManager.LoadScene(currentScene.buildIndex);
    }
    public static void GameOver()
    {
        QuitGame();
    }

    private static void QuitGame()
    {
#if UNITY_EDITOR
// Application.Quit() does not work in the editor
// so we use this instead
        UnityEditor.EditorApplication.isPlaying = false;
#else
// Close the game!
Application.Quit();
#endif
    }
}
