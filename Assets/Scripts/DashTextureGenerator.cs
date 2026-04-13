using System.IO;
using UnityEngine;

public class DashTextureGenerator : MonoBehaviour
{
    [ContextMenu("Generate Dash Texture")]
    public void GenerateTexture()
    {
        int width = 64;
        int height = 16;
        Texture2D tex = new Texture2D(width, height, TextureFormat.RGBA32, false);

        // צביעת חצי בצבע לבן אטום, וחצי בשקוף לחלוטין
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (x < width / 2)
                    tex.SetPixel(x, y, Color.white); // לבן לקו
                else
                    tex.SetPixel(x, y, Color.clear); // שקוף לרווח
            }
        }
        
        tex.Apply();

        // שמירת הקובץ לתיקיית המשחק
        byte[] bytes = tex.EncodeToPNG();
        File.WriteAllBytes(Application.dataPath + "/DashTexture.png", bytes);
        Debug.Log("Dash Texture Saved to: " + Application.dataPath);
    }
}