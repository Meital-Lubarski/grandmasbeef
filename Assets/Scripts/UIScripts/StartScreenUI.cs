using UnityEngine;

public class StartScreenUI : MonoBehaviour
{
    [SerializeField] private GameObject startScreenCanvas;

    private void OnEnable()
    {
        EventManagement.SwitchToGameOverScreen += TurnStartScreenOff;
        EventManagement.SwitchToStartScreen += TurnStartScreenOn;
        EventManagement.SwitchToGameScreen += TurnStartScreenOff;
        EventManagement.SwitchToNamePickScreen += TurnStartScreenOff;
        EventManagement.SwitchToPauseScreen += TurnStartScreenOff;
    }

    private void OnDisable()
    {
        EventManagement.SwitchToGameOverScreen -= TurnStartScreenOff;
        EventManagement.SwitchToStartScreen -= TurnStartScreenOn;
        EventManagement.SwitchToGameScreen -= TurnStartScreenOff;
        EventManagement.SwitchToNamePickScreen -= TurnStartScreenOff;
        EventManagement.SwitchToPauseScreen -= TurnStartScreenOff;
    }

    private void TurnStartScreenOff()
    {
        startScreenCanvas.SetActive(false);
    }

    private void TurnStartScreenOn()
    {
        startScreenCanvas.SetActive(true);
    }
}