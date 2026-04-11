using UnityEngine;

public class MapManager : MonoSingleton<MapManager>
{
    [SerializeField] private GameObject[] maps;

    private int _currentRoundIndex;

    public int CurrentRoundNumber => _currentRoundIndex + 1;
    public int TotalRounds => maps.Length;

    private void Start()
    {
        _currentRoundIndex = 0;
        ShowCurrentMap();
    }

    public void StartNewMatch()
    {
        _currentRoundIndex = 0;
        ShowCurrentMap();
    }

    public void AdvanceToNextRound()
    {
        if (_currentRoundIndex < maps.Length - 1)
        {
            _currentRoundIndex++;
            ShowCurrentMap();
        }
    }

    public bool HasMoreRounds()
    {
        return _currentRoundIndex < maps.Length - 1;
    }

    private void ShowCurrentMap()
    {
        for (int i = 0; i < maps.Length; i++)
        {
            if (maps[i] != null)
            {
                maps[i].SetActive(i == _currentRoundIndex);
            }
        }
    }
}