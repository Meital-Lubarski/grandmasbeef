using UnityEngine;

public class UIScreenMusic : MonoBehaviour
{
    [SerializeField] private AudioClip uiMusic;

    private void OnEnable()
    {
        EventManagement.SwitchToStartScreen += PlayUIMusic;
        EventManagement.SwitchToNamePickScreen += PlayUIMusic;
        EventManagement.SwitchToPauseScreen += PlayUIMusic;
        EventManagement.SwitchToGameOverScreen += PlayUIMusic;
        EventManagement.SwitchToGameScreen += StopUIMusic;
    }

    private void OnDisable()
    {
        EventManagement.SwitchToStartScreen -= PlayUIMusic;
        EventManagement.SwitchToNamePickScreen -= PlayUIMusic;
        EventManagement.SwitchToPauseScreen -= PlayUIMusic;
        EventManagement.SwitchToGameOverScreen -= PlayUIMusic;
        EventManagement.SwitchToGameScreen -= StopUIMusic;
    }

    private void Start()
    {
        PlayUIMusic();
    }

    private void PlayUIMusic()
    {
        if (uiMusic == null)
        {
            return;
        }

        AudioSource musicSource = SoundManager.Instance.MusicAudioSource;

        if (musicSource == null)
        {
            return;
        }

        if (musicSource.isPlaying && musicSource.clip == uiMusic)
        {
            return;
        }

        SoundManager.Instance.PlayMusic(uiMusic);
    }

    private void StopUIMusic()
    {
        SoundManager.Instance.StopMusic();
    }
}