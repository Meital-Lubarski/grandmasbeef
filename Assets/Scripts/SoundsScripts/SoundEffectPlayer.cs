using UnityEngine;

public class SoundEffectPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip clipToPlay;
    [SerializeField] private bool usePool = true;
    [SerializeField] private float volume = 1f;

    public void Play()
    {
        if (clipToPlay == null)
        {
            return;
        }

        if (usePool)
        {
            SoundManager.Instance.PlayPooledSound(clipToPlay, transform.position, volume);
        }
        else
        {
            SoundManager.Instance.PlayUISound(clipToPlay, volume);
        }
    }

    public void PlayClip(AudioClip clip)
    {
        if (clip == null)
        {
            return;
        }
        if (usePool)
        {
            SoundManager.Instance.PlayPooledSound(clip, transform.position, volume);
        }
        else
        {
            SoundManager.Instance.PlayUISound(clip, volume);
        }
    }
}