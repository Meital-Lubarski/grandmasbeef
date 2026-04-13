using UnityEngine;

public class GameScreenUI : MonoBehaviour
{
    [SerializeField] private GameObject gameHudCanvas;

    private void OnEnable()
    {
        EventManagement.SwitchToGameScreen += TurnOn;
        EventManagement.SwitchToGameOverScreen += TurnOff;
        EventManagement.SwitchToStartScreen += TurnOff;
        EventManagement.SwitchToNamePickScreen += TurnOff;
        EventManagement.SwitchToPauseScreen += TurnOff;
    }

    private void OnDisable()
    {
        EventManagement.SwitchToGameScreen -= TurnOn;
        EventManagement.SwitchToGameOverScreen -= TurnOff;
        EventManagement.SwitchToStartScreen -= TurnOff;
        EventManagement.SwitchToNamePickScreen -= TurnOff;
        EventManagement.SwitchToPauseScreen -= TurnOff;
    }

    private void TurnOff() { gameHudCanvas.SetActive(false); }
    private void TurnOn() { gameHudCanvas.SetActive(true); }
}