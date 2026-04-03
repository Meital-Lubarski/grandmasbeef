using System;
using UnityEngine;

public class ShootingHit : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        PlayerHitEffect hitEffect = other.collider.GetComponent<PlayerHitEffect>();
        PlayerMovement playerMovement = other.collider.GetComponent<PlayerMovement>();

        if (hitEffect != null && playerMovement != null)
        {
            string hitPlayerId = playerMovement.PlayerId;
            Debug.Log("Hit the player: " + hitPlayerId);
            
            Vector3 hitDirection = transform.up; 
            hitEffect.TakeHit(hitDirection); 
            
            EventManagement.OnPlayerHit?.Invoke(hitPlayerId, hitDirection);
            
            Destroy(gameObject);
        }
    }
}
