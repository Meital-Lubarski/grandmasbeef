using System;
using System.Collections;
using UnityEngine;

public class PlayerHitEffect : MonoBehaviour
{
    [Header("Hit Effect Settings")]
    [SerializeField] private float hitDuration = 1f; // משך זמן האפקט
    [SerializeField] private float spinSpeed = 1000f; // מהירות הסחרור
    [SerializeField] private float knockbackSpeed = 2f; // עוצמת ההדף

    private PlayerMovement _playerMovement;
    private bool _isStunned = false;

    private Rigidbody2D _rb;
    
    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _rb = GetComponent<Rigidbody2D>();

    }

    public void TakeHit(Vector3 knockbackDirection)
    {
        if (!_isStunned)
        {
            StartCoroutine(HitRoutine(knockbackDirection));
        }
    }

    private IEnumerator HitRoutine(Vector3 knockbackDirection)
    {
        _isStunned = true;
        _playerMovement.CanMove = false;

        float timer = 0f;

        while (timer < hitDuration)
        {
            float rotation = spinSpeed * Time.fixedDeltaTime;
            _rb.MoveRotation(_rb.rotation + rotation);

            Vector2 knockback = (Vector2)knockbackDirection * (knockbackSpeed * Time.fixedDeltaTime);
            _rb.MovePosition(_rb.position + knockback);

            timer += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }

        _rb.linearVelocity = Vector2.zero;
        _rb.angularVelocity = 0f;

        _playerMovement.CanMove = true;
        _isStunned = false;
    }
}
