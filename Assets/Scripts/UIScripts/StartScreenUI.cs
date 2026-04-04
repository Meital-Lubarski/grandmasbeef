using UnityEngine;

public class StartScreenUI : MonoBehaviour
{
    [SerializeField] private GameObject startPanel;

    private void OnEnable()
    {
        EventManagement.OnGameStarted += HandleGameStarted;
    }

    private void OnDisable()
    {
        EventManagement.OnGameStarted -= HandleGameStarted;
    }

    private void Start()
    {
        startPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    private void HandleGameStarted()
    {
        startPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void OnStartPressed()
    {
        GameManager.Instance.StartGame();
    }

    public void OnQuitPressed()
    {
        GameManager.QuitGame();
    }
}