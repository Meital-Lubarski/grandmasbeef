using UnityEngine;

public class SoundManager : MonoSingleton<SoundManager>
{
    [SerializeField] private AudioSource uiAudioSource;
    [SerializeField] private AudioSource musicAudioSource;

    public void PlayPooledSound(AudioClip clip, Vector3 position, float volume = 1f)
    {
        if (clip == null)
        {
            return;
        }
        PooledSound pooledSound = SimplePool<PooledSound>.Instance.Get();
        pooledSound.Play(clip, position, volume);
    }

    public void PlaySound(AudioClip clip)
    {
        PlayPooledSound(clip, Vector3.zero, 1f);
    }

    public void PlayUISound(AudioClip clip, float volume = 1f)
    {
        if (clip == null || uiAudioSource == null)
        {
            return;
        }
        uiAudioSource.PlayOneShot(clip, volume);
    }

    public void PlayMusic(AudioClip clip, bool loop = true, float volume = 1f)
    {
        if (clip == null || musicAudioSource == null)
        {
            return;
        }
        musicAudioSource.clip = clip;
        musicAudioSource.loop = loop;
        musicAudioSource.volume = volume;
        musicAudioSource.Play();
    }

    public void StopMusic()
    {
        if (musicAudioSource == null)
        {
            return;
        }
        musicAudioSource.Stop();
    }
}