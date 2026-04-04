using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PooledSound : MonoBehaviour, IPoolable
{
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void Reset()
    {
        StopAllCoroutines();
        _audioSource.Stop();
        _audioSource.clip = null;
        _audioSource.loop = false;
        _audioSource.volume = 1f;
        _audioSource.pitch = 1f;
        transform.position = Vector3.zero;
    }

    public void Play(AudioClip clip, Vector3 position, float volume = 1f)
    {
        if (clip == null)
        {
            return;
        }

        transform.position = position;
        _audioSource.clip = clip;
        _audioSource.volume = volume;
        _audioSource.Play();
        StartCoroutine(ReturnWhenFinished());
    }

    private IEnumerator ReturnWhenFinished()
    {
        yield return new WaitWhile(() => _audioSource.isPlaying);
        SimplePool<PooledSound>.Instance.Return(this);
    }
}