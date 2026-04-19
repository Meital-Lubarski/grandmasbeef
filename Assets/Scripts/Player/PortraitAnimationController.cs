using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PortraitAnimationController : MonoBehaviour
{
    private static readonly int PlayerHit = Animator.StringToHash("PlayerHit");
    
    private enum PlayerPortrait { Player1, Player2 }
    [SerializeField] private PlayerPortrait selectedPlayer;
    [SerializeField] private Animator animator;
    private void OnEnable()
    {
        EventManagement.OnPlayerHit += ActivateHitTrigger;
    }

    private void OnDisable()
    {
        EventManagement.OnPlayerHit -= ActivateHitTrigger;
    }
    
    private void ActivateHitTrigger(string hitPlayerId, Vector3 hitDirection)
    {
        if (selectedPlayer.ToString().Equals(hitPlayerId, StringComparison.OrdinalIgnoreCase))
        {
            if (animator != null)
            {
                animator.SetTrigger(PlayerHit);
            }
        }
    }
}