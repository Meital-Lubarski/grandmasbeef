using UnityEngine;

public class StartScreenUI : MonoBehaviour
{
    [SerializeField] private GameObject startCanvas;
    [SerializeField] private GameObject playerChooseCanvas;
    
    private void OnEnable()
    {
        EventManagement.OnGameStarted += HandleGameStarted;
    }

    private void OnDisable()
    {
        EventManagement.OnGameStarted -= HandleGameStarted;
    }

    private void ShowStartPanel()
    {
        startCanvas.SetActive(true);
        Time.timeScale = 0f;
    }

    private void HandleGameStarted()
    {
        startCanvas.SetActive(false);
        Time.timeScale = 1f;
    }

    public void OnStartPressed()
    {
        GameManager.Instance.StartGame();
    }
}