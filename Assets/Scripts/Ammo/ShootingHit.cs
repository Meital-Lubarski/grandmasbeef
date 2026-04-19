using System;
using UnityEngine;

public class ShootingHit : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        PlayerHitEffect hitEffect = other.collider.GetComponentInParent<PlayerHitEffect>();
        PlayerMovement playerMovement = other.collider.GetComponentInParent<PlayerMovement>();

        if (playerMovement != null)
        {
            string hitPlayerId = playerMovement.PlayerId;
            Debug.Log("Hit the player: " + hitPlayerId);

            Vector3 hitDirection = transform.up;

            if (hitEffect != null)
            {
                hitEffect.TakeHit(hitDirection);
            }

            EventManagement.OnPlayerHit?.Invoke(hitPlayerId, hitDirection);

            Destroy(gameObject);
        }
    }
}