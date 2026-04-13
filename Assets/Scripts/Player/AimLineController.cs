using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Shooting))]
public class AimLineController : MonoBehaviour
{
    [Header("Aiming Settings")]
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float maxAimDistance = 5f;
    [SerializeField] private LayerMask collisionLayers;
    [SerializeField] private int maxBounces = 3; // מגביל את כמות השבירות כדי למנוע קריסה (לולאה אינסופית)

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

        // רשימה שתשמור את כל הנקודות של הקו (התחלה + כל השבירות + סיום)
        List<Vector3> linePoints = new List<Vector3> { currentPosition };

        for (int i = 0; i < maxBounces; i++)
        {
            // כאן הקסם של השחקן: בירייה הראשונה נתעלם ממנו, אבל אחרי שבירה (i > 0) נאפשר פגיעה בו
            Collider2D colliderToIgnore = (i == 0) ? _myCollider : null;

            RaycastHit2D[] hits = Physics2D.RaycastAll(currentPosition, currentDirection, remainingDistance, collisionLayers);
            
            RaycastHit2D validHit = new RaycastHit2D();
            float closestDistance = float.MaxValue;
            bool foundHit = false;

            foreach (var hit in hits)
            {
                // מסננים: לא פוגעים בקוליידר שאנחנו מתעלמים ממנו, ולא פוגעים בנקודה שממנה הרגע יצאנו (distance > 0.01f)
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
                // מצאנו פגיעה! מוסיפים את הנקודה לקו ומורידים את המרחק שעברנו
                linePoints.Add(validHit.point);
                remainingDistance -= validHit.distance;

                // בודקים אם פגענו בקיר (חובה להגדיר את הקירות ביוניטי עם התגית "Wall")
                if (validHit.collider.CompareTag("Wall"))
                {
                    // Vector2.Reflect מחשב לאן הקרן צריכה להישבר בהתאם לזווית של הקיר (הנורמל)
                    currentDirection = Vector2.Reflect(currentDirection, validHit.normal);
                    currentPosition = validHit.point;
                    // הלולאה ממשיכה לשבירה הבאה...
                }
                else 
                {
                    // פגענו במשהו שאינו קיר (למשל שחקן אחר, או השחקן שלנו אחרי חזרה). הקו נעצר!
                    break;
                }
            }
            else
            {
                // לא פגענו בכלום, מותחים את הקו עד סוף המרחק שנשאר באוויר
                linePoints.Add(currentPosition + (currentDirection * remainingDistance));
                break; 
            }
            
            // אם ניצלנו את כל המרחק המקסימלי, הלולאה עוצרת
            if (remainingDistance <= 0f) break;
        }

        // מעדכנים את ה-Line Renderer עם כמות הנקודות והמיקומים שלהן
        lineRenderer.positionCount = linePoints.Count;
        lineRenderer.SetPositions(linePoints.ToArray());

        // שומרים על קווקווים אחידים לאורך כל הקו השבור
        if (lineRenderer.material != null)
        {
            float totalDistance = 0f;
            for (int j = 0; j < linePoints.Count - 1; j++)
            {
                totalDistance += Vector3.Distance(linePoints[j], linePoints[j + 1]);
            }
            // המספר 2f הוא מקדם הצפיפות של הקווקווים - תשחקי איתו כדי להגיע למראה מושלם
            lineRenderer.material.mainTextureScale = new Vector2(totalDistance * 2f, 1);
        }
    }
}