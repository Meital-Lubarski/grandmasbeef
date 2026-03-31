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

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void OnEnable()
    {
        EventManagement.OnPlayerHit += TakeHit;
    }
    private void OnDisable()
    {
        EventManagement.OnPlayerHit -= TakeHit;
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
            transform.Rotate(Vector3.forward * (spinSpeed * Time.deltaTime));
            transform.position += knockbackDirection * (knockbackSpeed * Time.deltaTime);

            timer += Time.deltaTime;
            yield return null;
        }
        _playerMovement.CanMove = true; 
        _isStunned = false;
    }
}
