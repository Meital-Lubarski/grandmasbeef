using TMPro;
using UnityEngine;

public class AmmoUIXXX : MonoBehaviour
{
    [SerializeField] private TMP_Text player1AmmoText;
    [SerializeField] private TMP_Text player2AmmoText;

    private void OnEnable()
    {
        EventManagement.OnAmmoChanged += UpdateAmmoUI;
    }

    private void OnDisable()
    {
        EventManagement.OnAmmoChanged -= UpdateAmmoUI;
    }

    private void UpdateAmmoUI(string playerId, int currentAmmo)
    {
        if (playerId == "Player1")
        {
            player1AmmoText.text = "P1 Ammo: " + currentAmmo;
        }
        else if (playerId == "Player2")
        {
            player2AmmoText.text = "P2 Ammo: " + currentAmmo;
        }
    }
}