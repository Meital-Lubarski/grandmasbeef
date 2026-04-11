using UnityEngine;

public class HitSoundUI : MonoBehaviour
{
    [SerializeField] private AudioClip[] hitClips;
    [SerializeField] private float volume = 1f;

    private void OnEnable()
    {
        EventManagement.OnPlayerHit += PlayRandomHitSound;
    }

    private void OnDisable()
    {
        EventManagement.OnPlayerHit -= PlayRandomHitSound;
    }

    private void PlayRandomHitSound(string playerId, Vector3 hitPosition)
    {
        if (hitClips == null || hitClips.Length == 0)
        {
            return;
        }

        int randomIndex = Random.Range(0, hitClips.Length);
        AudioClip chosenClip = hitClips[randomIndex];

        SoundManager.Instance.PlayPooledSound(chosenClip, hitPosition, volume);
    }
}