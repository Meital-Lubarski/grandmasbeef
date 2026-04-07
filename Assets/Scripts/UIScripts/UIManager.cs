using UnityEngine;

public class UIManager : MonoBehaviour
{
    // --- מסך התחלה ---
    public void OnStartScreenPlayPressed()
    {
        // מעבר למסך בחירת שמות
        EventManagement.SwitchToNamePickScreen?.Invoke();
    }

    // --- מסך בחירת שמות ---
    public void OnNamePickStartBattlePressed()
    {
        // מתחילים את המשחק!
        EventManagement.SwitchToGameScreen?.Invoke();
    }

    // --- משחק (HUD) ---
    public void OnPausePressed()
    {
        // פתיחת תפריט עצירה
        EventManagement.SwitchToPauseScreen?.Invoke();
    }

    // --- תפריט עצירה ---
    public void OnResumePressed()
    {
        // חזרה למשחק
        EventManagement.SwitchToGameScreen?.Invoke();
    }

    public void OnExitToStartPressed()
    {
        // חזרה למסך הראשי
        EventManagement.SwitchToStartScreen?.Invoke();
    }

    // --- מסך Game Over ---
    public void OnRestartPressed()
    {
        // קריאה ל-GameManager שיתחיל מחדש את הסצנה
        GameManager.Instance.RestartCurrentScene();
    }

    // --- כללי ---
    public void OnQuitPressed()
    {
        GameManager.QuitGame();
    }
}
