using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

//Class for saving the names of the players in the game
public class PlayersNameUI : MonoBehaviour
{
    [SerializeField] private string defaultPlayer1Name = "Player 1";
    [SerializeField] private string defaultPlayer2Name = "Player 2";
    
    
    [SerializeField] private TMP_InputField player1InputField;
    [SerializeField] private TMP_InputField player2InputField;
    
    void Start()
    {
        player1InputField.text = PlayerPrefs.GetString("Player1Name", "Player 1");
        player2InputField.text = PlayerPrefs.GetString("Player2Name", "Player 2");
    }

    public void SavePlayerName()
    {
        string p1Name = string.IsNullOrWhiteSpace(player1InputField.text) ? defaultPlayer1Name : player1InputField.text;
        string p2Name = string.IsNullOrWhiteSpace(player2InputField.text) ? defaultPlayer2Name : player2InputField.text;

        PlayerPrefs.SetString("Player1Name", p1Name);
        PlayerPrefs.SetString("Player2Name", p2Name);
        
        PlayerPrefs.Save(); 
        
        Debug.Log($"The names has been saved: {p1Name} against {p2Name}");
    }
}
