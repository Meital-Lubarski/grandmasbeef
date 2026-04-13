using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Shooting))]
public class AimLineController : MonoBehaviour
{
    [Header("Aiming Settings")]
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float maxAimDistance = 5f;
    [SerializeField] private LayerMask collisionLayers;
    [SerializeField] private int maxBounces = 3; //Max bounces to prevent falling apart
    [SerializeField] private string wallTag = "Wall";
    
    [Tooltip("Match this to your bullet's collider radius")]
    [SerializeField] private float bulletRadius = 0.25f;
    
    private Shooting _shooting;
    private Collider2D _myCollider; 

    private void Awake()
    {
        _shooting = GetComponent<Shooting>();
        _myCollider = GetComponent<Collider2D>();
        
        if (lineRenderer != null)
        {
            lineRenderer.enabled = false;
        }
    }

    private void Update()
    {
        if (lineRenderer == null) return;

        if (_shooting.IsCharging)
        {
            if (!lineRenderer.enabled) lineRenderer.enabled = true;
            UpdateAimLine();
        }
        else
        {
            if (lineRenderer.enabled) lineRenderer.enabled = false;
        }
    }

    private void UpdateAimLine()
    {
        float remainingDistance = maxAimDistance;
        Vector2 currentPosition = _shooting.ShootPoint.position;
        Vector2 currentDirection = _shooting.transform.up;
        
        //Saving all the points of the line (Start + break points + finish)
        List<Vector3> linePoints = new List<Vector3> { currentPosition };

        for (int i = 0; i < maxBounces; i++)
        {
            Collider2D colliderToIgnore = (i == 0) ? _myCollider : null;

            RaycastHit2D[] hits = Physics2D.CircleCastAll(currentPosition, bulletRadius, currentDirection, remainingDistance, collisionLayers);
            
            RaycastHit2D validHit = new RaycastHit2D();
            float closestDistance = float.MaxValue;
            bool foundHit = false;

            foreach (var hit in hits)
            {
                if (hit.collider != null && hit.collider != colliderToIgnore && hit.distance > 0.01f)
                {
                    if (hit.distance < closestDistance)
                    {
                        closestDistance = hit.distance;
                        validHit = hit;
                        foundHit = true;
                    }
                }
            }

            if (foundHit)
            {
                linePoints.Add(validHit.point);
                remainingDistance -= validHit.distance;

                if (validHit.collider.CompareTag(wallTag))
                {
                    currentDirection = Vector2.Reflect(currentDirection, validHit.normal);
                    currentPosition = validHit.point;
                }
                else 
                {
                    break;
                }
            }
            else
            {
                linePoints.Add(currentPosition + (currentDirection * remainingDistance));
                break; 
            }
            
            if (remainingDistance <= 0f) break;
        }

        lineRenderer.positionCount = linePoints.Count;
        lineRenderer.SetPositions(linePoints.ToArray());
    }
}