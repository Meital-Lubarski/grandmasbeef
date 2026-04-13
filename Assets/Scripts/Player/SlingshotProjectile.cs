using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class SlingshotProjectile : MonoBehaviour
{
    [Header("Lifetime")]
    [SerializeField] private float lifeTime = 5f;

    private Rigidbody2D _rb;
    private Collider2D _projectileCollider;
    private Shooting _owner;
    

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        
        _projectileCollider = GetComponent<Collider2D>();
        
        _rb.gravityScale = 0f;
        _rb.linearDamping = 0f;
        _rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }

    public void Initialize(Vector2 direction, float launchForce, Shooting owner, Collider2D shooterCollider)
    {
        _owner = owner;
        _rb.linearVelocity = Vector2.zero;
        _rb.angularVelocity = 0f;
        _rb.AddForce(direction * launchForce, ForceMode2D.Impulse);
        if (shooterCollider != null)
        {
            StartCoroutine(IgnoreSelfCollisionRoutine(shooterCollider));
        }

        EventManagement.OnSlingshotLaunched?.Invoke(_rb.linearVelocity);

        Destroy(gameObject, lifeTime);
    }
    
    private IEnumerator IgnoreSelfCollisionRoutine(Collider2D shooterCollider)
    {
        Physics2D.IgnoreCollision(_projectileCollider, shooterCollider, true);
        yield return new WaitForSeconds(0.15f);
        if (_projectileCollider != null && shooterCollider != null)
        {
            Physics2D.IgnoreCollision(_projectileCollider, shooterCollider, false);
        }
    }

    private void OnDestroy()
    {
        EventManagement.OnProjectileResolved?.Invoke(); 
        if (_owner != null)
        {
            _owner.NotifyProjectileDestroyed();
        }
    }
}