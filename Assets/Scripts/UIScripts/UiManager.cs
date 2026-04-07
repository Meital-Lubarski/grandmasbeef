using UnityEngine;

public class UiManager : MonoBehaviour
{
    private void OnPickNamePress()
    {
        EventManagement.SwitchToNamePickScreen?.Invoke();
    }

    private void OnGameStart()
    {
        EventManagement.SwitchToGameScreen?.Invoke();
    }

    private void OnPausePress()
    {
        EventManagement.SwitchToPauseScreen?.Invoke();
    }

    private void OnResumePress()
    {
        
    }
}
