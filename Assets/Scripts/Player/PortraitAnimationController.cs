using UnityEngine;
using UnityEngine.Serialization;

public class PortraitAnimationController : MonoBehaviour
{
    private static readonly int PlayerHit = Animator.StringToHash("PlayerHit");
    [SerializeField] private Animator player1Animator;
    [SerializeField] private Animator player2Animator;
    private const string Player1Id = "Player1";
    private const string Player2Id = "Player2";

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
        if (hitPlayerId == Player1Id)
        {
            HitPlayer(player1Animator);
        }
        else if (hitPlayerId == Player2Id)
        {
            HitPlayer(player2Animator);
        }
    }

    private void HitPlayer(Animator playerAnimator)
    {
        if (playerAnimator != null)
        {
            playerAnimator.SetTrigger(PlayerHit);
        }
    }
}