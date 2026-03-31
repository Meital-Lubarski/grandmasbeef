using UnityEngine;

public class ShootingHit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHitEffect hitEffect = collision.GetComponent<PlayerHitEffect>();
        if (hitEffect != null)
        {
            Vector3 hitDirection = transform.up; 
            EventManagement.OnPlayerHit.Invoke(hitDirection);
            Destroy(gameObject);
        }
    }
}
