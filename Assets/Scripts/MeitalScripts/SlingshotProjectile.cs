using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class SlingshotProjectile : MonoBehaviour
{
    [Header("Lifetime")]
    [SerializeField] private float lifeTime = 5f;

    private Rigidbody2D _rb;
    private Shooting _owner;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0f;
        _rb.linearDamping = 0f;
        _rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }

    public void Initialize(Vector2 direction, float launchForce, Shooting owner)
    {
        _owner = owner;
        _rb.linearVelocity = Vector2.zero;
        _rb.angularVelocity = 0f;
        _rb.AddForce(direction * launchForce, ForceMode2D.Impulse);

        EventManagement.OnSlingshotLaunched?.Invoke(_rb.linearVelocity);

        Destroy(gameObject, lifeTime);
    }

    private void OnDestroy()
    {
        if (_owner != null)
        {
            _owner.NotifyProjectileDestroyed();
        }
    }
}