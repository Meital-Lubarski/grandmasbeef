using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class SlingshotHead : MonoBehaviour
{
    [Header("Slingshot")]
    [SerializeField] private float maxDragDistance = 2.5f;
    [SerializeField] private float launchForceMultiplier = 12f;

    [Header("Camera Bounds")]
    [SerializeField] private float screenMargin = 0.3f;

    [Header("Stop Detection")]
    [SerializeField] private float stopVelocityThreshold = 0.1f;
    [SerializeField] private float stopTimeNeeded = 0.15f;

    private Rigidbody2D rb;
    private Collider2D objectCollider;
    private Camera mainCamera;

    private Vector2 launchStartPosition;
    private Vector2 currentPointerScreenPosition;
    private Vector2 currentDragOffset;

    private bool pointerHeld;
    private bool isDragging;
    private bool hasLaunched;
    private bool stopEventSent;
    private float stopTimer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        objectCollider = GetComponent<Collider2D>();
        mainCamera = Camera.main;
        launchStartPosition = transform.position;
        rb.gravityScale = 0f;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.linearDamping = 3f;
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    private void Update()
    {
        if (mainCamera == null)
        {
            return;
        }
        if (pointerHeld && isDragging)
        {
            Drag();
        }
        if (hasLaunched && !stopEventSent)
        {
            CheckIfStopped();
        }
    }
    
    
    private void LateUpdate()
    {
        if (!hasLaunched || mainCamera == null)
        {
            return;
        }

        Vector2 clampedPosition = ClampPositionToCamera(transform.position);

        if ((Vector2)transform.position != clampedPosition)
        {
            transform.position = clampedPosition;
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
    }

    public void OnPoint(InputValue value)
    {
        currentPointerScreenPosition = value.Get<Vector2>();
    }

    public void OnClick(InputValue value)
    {
        bool isPressed = value.isPressed;

        if (isPressed)
        {
            TryStartDragging();
            return;
        }
        if (isDragging)
        {
            Release();
        }
        pointerHeld = false;
    }

    private void TryStartDragging()
    {
        if (hasLaunched)
        {
            return;
        }
        Vector2 mouseWorldPosition = GetPointerWorldPosition();
        if (!objectCollider.OverlapPoint(mouseWorldPosition))
        {
            return;
        }
        pointerHeld = true;
        isDragging = true;
        currentDragOffset = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        EventManagement.OnSlingshotAimStarted?.Invoke();
    }

    private void Drag()
    {
        Vector2 pointerWorldPosition = GetPointerWorldPosition();
        Vector2 rawDragVector = pointerWorldPosition - launchStartPosition;
        currentDragOffset = GetFourDirectionDrag(rawDragVector);
        Vector2 draggedPosition = launchStartPosition + currentDragOffset;
        transform.position = ClampPositionToCamera(draggedPosition);
        Vector2 launchDirection = (-currentDragOffset).normalized;
        float powerPercent = currentDragOffset.magnitude / maxDragDistance;
        EventManagement.OnSlingshotAiming?.Invoke(launchDirection, powerPercent);
    }

    private Vector2 GetFourDirectionDrag(Vector2 rawDragVector)
    {
        if (rawDragVector == Vector2.zero)
        {
            return Vector2.zero;
        }
        if (Mathf.Abs(rawDragVector.x) >= Mathf.Abs(rawDragVector.y))
        {
            Vector2 snappedDirection = rawDragVector.x >= 0f ? Vector2.right : Vector2.left;
            float distance = Mathf.Min(Mathf.Abs(rawDragVector.x), maxDragDistance);
            return snappedDirection * distance;
        }
        Vector2 verticalDirection = rawDragVector.y >= 0f ? Vector2.up : Vector2.down;
        float verticalDistance = Mathf.Min(Mathf.Abs(rawDragVector.y), maxDragDistance);
        return verticalDirection * verticalDistance;
    }

    private void Release()
    {
        pointerHeld = false;
        isDragging = false;
        hasLaunched = true;
        stopEventSent = false;
        stopTimer = 0f;
        Launch();
    }

    private void Launch()
    {
        Vector2 launchVector = -currentDragOffset;
        if (launchVector == Vector2.zero)
        {
            hasLaunched = false;
            return;
        }
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.AddForce(launchVector * launchForceMultiplier, ForceMode2D.Impulse);
        EventManagement.OnSlingshotLaunched?.Invoke(rb.linearVelocity);
    }
    

    private Vector2 ClampPositionToCamera(Vector2 position)
    {
        float camHeight = mainCamera.orthographicSize * 2f;
        float camWidth = camHeight * mainCamera.aspect;

        Vector2 camCenter = mainCamera.transform.position;

        float minX = camCenter.x - camWidth / 2f + screenMargin;
        float maxX = camCenter.x + camWidth / 2f - screenMargin;
        float minY = camCenter.y - camHeight / 2f + screenMargin;
        float maxY = camCenter.y + camHeight / 2f - screenMargin;

        float clampedX = Mathf.Clamp(position.x, minX, maxX);
        float clampedY = Mathf.Clamp(position.y, minY, maxY);

        return new Vector2(clampedX, clampedY);
    }

    private void CheckIfStopped()
    {
        if (rb.linearVelocity.magnitude <= stopVelocityThreshold)
        {
            stopTimer += Time.deltaTime;

            if (stopTimer >= stopTimeNeeded)
            {
                stopEventSent = true;

                rb.linearVelocity = Vector2.zero;
                rb.angularVelocity = 0f;
                rb.bodyType = RigidbodyType2D.Kinematic;

                hasLaunched = false;
                launchStartPosition = transform.position;
                currentDragOffset = Vector2.zero;

                EventManagement.OnSlingshotStopped?.Invoke(transform.position);
            }
        }
        else
        {
            stopTimer = 0f;
        }
    }

    private Vector2 GetPointerWorldPosition()
    {
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(
            new Vector3(
                currentPointerScreenPosition.x,
                currentPointerScreenPosition.y,
                Mathf.Abs(mainCamera.transform.position.z))
        );

        return new Vector2(worldPosition.x, worldPosition.y);
    }

    public void ResetSlingshot()
    {
        pointerHeld = false;
        isDragging = false;
        hasLaunched = false;
        stopEventSent = false;
        stopTimer = 0f;
        currentDragOffset = Vector2.zero;

        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;

        transform.position = launchStartPosition;
    }
}