using UnityEngine;

public class AmmoManager : MonoSingleton<AmmoManager>
{
    [Header("Ammo Settings")]
    public int maxAmmoPerRound = 6;
    
    private int _player1Ammo;
    private int _player2Ammo;
    private void OnEnable()
    {
        ResetAmmo();
        EventManagement.SwitchToGameScreen += ResetAmmo;
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

    public bool CanPlayerShoot(string playerId)
    {
        bool canShoot = false;
        
        if (playerId == "Player1") 
            canShoot = _player1Ammo > 0;
        else if (playerId == "Player2") 
            canShoot = _player2Ammo > 0;
            
        Debug.Log($"Checking ammo for '{playerId}': P1 Ammo = {_player1Ammo}, P2 Ammo = {_player2Ammo}. Can shoot? {canShoot}");
        
        return canShoot;
    }

    private void CheckIfBothOutOfAmmo()
    {
        if (_player1Ammo <= 0 && _player2Ammo <= 0)
        {
            Debug.Log("לשני השחקנים נגמרה התחמושת! מסיימים את הסיבוב מוקדם.");
            EventManagement.OnTimerComplete?.Invoke();
        }
    }
}