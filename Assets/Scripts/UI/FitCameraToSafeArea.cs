using UnityEngine;

public class FitCameraToSafeArea : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [Header("How many world Unity units fit into the screen width")]
    [SerializeField] private float width = 19.2f;

    private void Awake()
    {
        // אם לא הוגדרה מצלמה, ניקח את המצלמה שיושבת על האובייקט
        if (cam == null)
            cam = GetComponent<Camera>();
        
        if (!cam.orthographic) 
            Debug.LogWarning("Camera is not orthographic, this script is designed for orthographic cameras");
    }

    private void Start()
    {
        FitToWidth();
    }

    private void FitToWidth()
    {
        float ratio;

        // נבדוק אם המצלמה אכן מרנדרת ל-RenderTexture
        if (cam.targetTexture != null)
        {
            // ניקח את היחס מתוך ה-RenderTexture עצמו
            ratio = (float)cam.targetTexture.width / cam.targetTexture.height;
        }
        else
        {
            // רק למקרה חירום בו אין RenderTexture, ניקח מהמסך
            ratio = (float)Screen.width / Screen.height;
        }

        // חישוב ישיר ונקי של ה-Orthographic Size
        // Orthographic Size הוא חצי מהגובה. 
        // גובה = רוחב חלקי היחס.
        cam.orthographicSize = width / (2f * ratio);
    }
}