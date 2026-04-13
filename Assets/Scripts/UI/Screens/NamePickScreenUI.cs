using UnityEngine;

public class NamePickScreenUI : MonoBehaviour
{
    [SerializeField] private GameObject namePickCanvas;

    private void OnEnable()
    {
        EventManagement.SwitchToNamePickScreen += TurnOn;
        EventManagement.SwitchToGameOverScreen += TurnOff;
        EventManagement.SwitchToStartScreen += TurnOff;
        EventManagement.SwitchToGameScreen += TurnOff;
        EventManagement.SwitchToPauseScreen += TurnOff;
    }

    private void OnDisable()
    {
        EventManagement.SwitchToNamePickScreen -= TurnOn;
        EventManagement.SwitchToGameOverScreen -= TurnOff;
        EventManagement.SwitchToStartScreen -= TurnOff;
        EventManagement.SwitchToGameScreen -= TurnOff;
        EventManagement.SwitchToPauseScreen -= TurnOff;
    }

    private void TurnOff() { namePickCanvas.SetActive(false); }
    private void TurnOn() { namePickCanvas.SetActive(true); }
}