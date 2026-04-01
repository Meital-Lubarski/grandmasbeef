using TMPro;
using UnityEngine;

//Class for saving the names of the players in the game
public class PlayersNameUI : MonoBehaviour
{
    public TMP_InputField nameInputField;

    void Start()
    {
        // כשהמשחק מתחיל, נבדוק אם כבר שמרנו שם בעבר
        if (PlayerPrefs.HasKey("PlayerName"))
        {
            // אם כן, נטען אותו ונציג אותו בתיבת הטקסט
            nameInputField.text = PlayerPrefs.GetString("PlayerName");
        }
    }

    // פונקציה שתופעל כשהשחקן ילחץ על כפתור השמירה
    public void SavePlayerName()
    {
        // שומרים את הטקסט שנכתב בתיבה לתוך PlayerPrefs תחת המפתח "PlayerName"
        PlayerPrefs.SetString("PlayerName", nameInputField.text);
        
        // שומרים פיזית את הנתונים כדי להבטיח שלא יאבדו
        PlayerPrefs.Save(); 
        
        Debug.Log("השם נשמר בהצלחה: " + nameInputField.text);
    }
}
