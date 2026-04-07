using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{

    private void PauseTimeScale()
    {
        Time.timeScale = 0f;
    }

    private void ContinueTimeScale()
    {
        Time.timeScale = 1f;
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