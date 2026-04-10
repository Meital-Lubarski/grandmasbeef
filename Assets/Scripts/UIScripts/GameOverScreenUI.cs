using UnityEngine;

public class GameOverScreenUI : MonoBehaviour
{
    [SerializeField] private GameObject gameOverCanvas;

    private void OnEnable()
    {
        EventManagement.SwitchToGameOverScreen += TurnGameOverOn;
        EventManagement.SwitchToStartScreen += TurnGameOverOff;
        EventManagement.SwitchToGameScreen += TurnGameOverOff;
        EventManagement.SwitchToNamePickScreen += TurnGameOverOff;
        EventManagement.SwitchToPauseScreen += TurnGameOverOff;
    }

    private void OnDisable()
    {
        EventManagement.SwitchToGameOverScreen -= TurnGameOverOn;
        EventManagement.SwitchToStartScreen -= TurnGameOverOff;
        EventManagement.SwitchToGameScreen -= TurnGameOverOff;
        EventManagement.SwitchToNamePickScreen -= TurnGameOverOff;
        EventManagement.SwitchToPauseScreen -= TurnGameOverOff;
    }

    private void TurnGameOverOff()
    {
        gameOverCanvas.SetActive(false);
    }

    private void TurnGameOverOn()
    {
        gameOverCanvas.SetActive(true);
    }
}