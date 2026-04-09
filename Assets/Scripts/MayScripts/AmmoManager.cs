using UnityEngine;

public class AmmoManager : MonoSingleton<AmmoManager>
{
    [Header("Ammo Settings")]
    public int maxAmmoPerRound = 6;
    
    private int _player1Ammo;
    private int _player2Ammo;

    private void OnEnable()
    {
        // נאפס את התחמושת בכל פעם שמתחילים משחק/סיבוב
        EventManagement.SwitchToGameScreen += ResetAmmo;
        // נקשיב לאירוע ירייה של שחקן
        EventManagement.OnPlayerShot += HandlePlayerShot;
    }

    private void OnDisable()
    {
        EventManagement.SwitchToGameScreen -= ResetAmmo;
        EventManagement.OnPlayerShot -= HandlePlayerShot;
    }

    private void ResetAmmo()
    {
        _player1Ammo = maxAmmoPerRound;
        _player2Ammo = maxAmmoPerRound;
        
        // נעדכן את ה-UI שהתחמושת התאפסה
        EventManagement.OnAmmoChanged?.Invoke("Player1", _player1Ammo);
        EventManagement.OnAmmoChanged?.Invoke("Player2", _player2Ammo);
    }

    private void HandlePlayerShot(string playerId)
    {
        if (playerId == "Player1" && _player1Ammo > 0)
        {
            _player1Ammo--;
            EventManagement.OnAmmoChanged?.Invoke("Player1", _player1Ammo);
        }
        else if (playerId == "Player2" && _player2Ammo > 0)
        {
            _player2Ammo--;
            EventManagement.OnAmmoChanged?.Invoke("Player2", _player2Ammo);
        }

        CheckIfBothOutOfAmmo();
    }

    // פונקציה שתעזור לסקריפט הירי שלך לדעת אם מותר לשחקן לירות בכלל
    public bool CanPlayerShoot(string playerId)
    {
        if (playerId == "Player1") return _player1Ammo > 0;
        if (playerId == "Player2") return _player2Ammo > 0;
        return false;
    }

    private void CheckIfBothOutOfAmmo()
    {
        if (_player1Ammo <= 0 && _player2Ammo <= 0)
        {
            Debug.Log("לשני השחקנים נגמרה התחמושת! מסיימים את הסיבוב מוקדם.");
            // קוראים לאותו איוונט שמסיים את הסיבוב כשהזמן נגמר
            EventManagement.OnTimerComplete?.Invoke();
        }
    }
}