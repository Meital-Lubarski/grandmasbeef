using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortraitChange : MonoBehaviour
{
    //Picking the player to switch its portrait
    private enum PlayerPortrait { Player1, Player2 }
    [SerializeField] private PlayerPortrait selectedPlayer;
    
    [Header("UI References")]
    [SerializeField] private Image displayImage; 
    
    [Header("Settings")]
    [SerializeField] private List<Sprite> sprites;

    [SerializeField] private int initialIndex = 0;
    private int _currentIndex = 0;


    void Start()
    {
        _currentIndex = initialIndex;
        UpdateUI();
    }

    private void OnEnable()
    {
        EventManagement.OnPlayerHit += ShowNextImage;
        EventManagement.ResetPoints += ResetSequence;
        EventManagement.OnRoundComplete += ResetSequence;
    }

    private void OnDisable()
    {
        EventManagement.OnPlayerHit -= ShowNextImage;
        EventManagement.ResetPoints -= ResetSequence;
        EventManagement.OnRoundComplete -= ResetSequence;
    }

    private void ResetSequence(string obj)
    {
        ResetSequence();
    }

    private void ShowNextImage(string hitPlayerId, Vector3 vector3)
    {
        if (!selectedPlayer.ToString().Equals(hitPlayerId, StringComparison.OrdinalIgnoreCase))
            return;
        if (sprites.Count == 0) return;

        _currentIndex++;
        
        if (_currentIndex >= sprites.Count)
        {
            _currentIndex = sprites.Count - 1;
        }

        UpdateUI();
    }

    private void ResetSequence()
    {
        _currentIndex = 0;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (displayImage != null && sprites.Count > 0)
        {
            displayImage.sprite = sprites[_currentIndex];
        }
    }
}
