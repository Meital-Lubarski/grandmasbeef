using UnityEngine;

public class AmmoManager : MonoSingleton<AmmoManager>
{
    [Header("Ammo Settings")]
    public int maxAmmoPerRound = 6;

    private int _player1Ammo;
    private int _player2Ammo;
    private int _activeProjectiles;
    private bool _roundEnded;

    private void OnEnable()
    {
        ResetAmmo();
        EventManagement.SwitchToGameScreen += ResetAmmo;
        EventManagement.OnPlayerShot += HandlePlayerShot;
        EventManagement.OnProjectileSpawned += HandleProjectileSpawned;
        EventManagement.OnProjectileResolved += HandleProjectileResolved;
    }

    private void OnDisable()
    {
        EventManagement.SwitchToGameScreen -= ResetAmmo;
        EventManagement.OnPlayerShot -= HandlePlayerShot;
        EventManagement.OnProjectileSpawned -= HandleProjectileSpawned;
        EventManagement.OnProjectileResolved -= HandleProjectileResolved;
    }

    private void ResetAmmo()
    {
        _player1Ammo = maxAmmoPerRound;
        _player2Ammo = maxAmmoPerRound;
        _activeProjectiles = 0;
        _roundEnded = false;

        EventManagement.OnAmmoChanged?.Invoke("Player1", _player1Ammo);
        EventManagement.OnAmmoChanged?.Invoke("Player2", _player2Ammo);
    }

    private void HandlePlayerShot(string playerId)
    {
        if (_roundEnded)
        {
            return;
        }

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

        CheckIfRoundShouldEnd();
    }

    private void HandleProjectileSpawned()
    {
        if (_roundEnded)
        {
            return;
        }

        _activeProjectiles++;
        Debug.Log("Projectile spawned. Active projectiles: " + _activeProjectiles);
    }

    private void HandleProjectileResolved()
    {
        if (_activeProjectiles > 0)
        {
            _activeProjectiles--;
        }

        Debug.Log("Projectile resolved. Active projectiles: " + _activeProjectiles);
        CheckIfRoundShouldEnd();
    }

    public bool CanPlayerShoot(string playerId)
    {
        bool canShoot = false;

        if (playerId == "Player1")
        {
            canShoot = _player1Ammo > 0;
        }
        else if (playerId == "Player2")
        {
            canShoot = _player2Ammo > 0;
        }

        Debug.Log($"Checking ammo for '{playerId}': P1 Ammo = {_player1Ammo}, P2 Ammo = {_player2Ammo}. Can shoot? {canShoot}");

        return canShoot;
    }

    private void CheckIfRoundShouldEnd()
    {
        bool bothOutOfAmmo = _player1Ammo <= 0 && _player2Ammo <= 0;
        bool noProjectilesLeft = _activeProjectiles <= 0;

        if (!_roundEnded && bothOutOfAmmo && noProjectilesLeft)
        {
            _roundEnded = true;
            Debug.Log("Both players are out of ammo and no projectiles remain. Ending round.");
            EventManagement.OnTimerComplete?.Invoke();
        }
    }
}