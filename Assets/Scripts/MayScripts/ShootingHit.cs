using System;
using UnityEngine;

public class ShootingHit : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        PlayerHitEffect hitEffect = other.collider.GetComponent<PlayerHitEffect>();
        if (hitEffect != null)
        {
            Debug.Log("Hit the player");
            Vector3 hitDirection = transform.up; 
            hitEffect.TakeHit(hitDirection); 
            Destroy(gameObject);
        }
    }
}
