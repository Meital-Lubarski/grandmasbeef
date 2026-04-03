using UnityEngine;
using UnityEngine.Android;

public class ScoreManager : MonoBehaviour
{
    private void OnEnable()
    {
        EventManagement.OnPlayerHit += HandlePlayerHit;
    }

    private void OnDisable()
    {
        EventManagement.OnPlayerHit -= HandlePlayerHit;
    }

    private void HandlePlayerHit(string hitPlayerId, Vector3 hitDirection)
    {
        if (hitPlayerId == "Player1")
        {
            //TODO: point for player 2 or minus life for player 1
        }
        else if (hitPlayerId == "Player2")
        {
            //TODO: point for player 1 or minus life for player 2
        }
    }
}
