using UnityEngine;

public class PauseMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuCanvas;

    private void OnEnable()
    {
        EventManagement.SwitchToGameOverScreen += TurnPauseOff;
        EventManagement.SwitchToStartScreen += TurnPauseOff;
        EventManagement.SwitchToGameScreen += TurnPauseOff;
        EventManagement.SwitchToNamePickScreen += TurnPauseOff;
        EventManagement.SwitchToPauseScreen += TurnPauseOn;
    }

    private void OnDisable()
    {
        EventManagement.SwitchToGameOverScreen -= TurnPauseOff;
        EventManagement.SwitchToStartScreen -= TurnPauseOff;
        EventManagement.SwitchToGameScreen -= TurnPauseOff;
        EventManagement.SwitchToNamePickScreen -= TurnPauseOff;
        EventManagement.SwitchToPauseScreen -= TurnPauseOn;
    }

    private void TurnPauseOff()
    {
        pauseMenuCanvas.SetActive(false);
    }

    private void TurnPauseOn()
    {
        pauseMenuCanvas.SetActive(true);
    }
}