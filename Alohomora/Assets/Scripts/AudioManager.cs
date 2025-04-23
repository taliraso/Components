using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource sfxSource;
    public AudioClip dialsMatchedClip;
    public AudioClip withinRangeClip;
    public AudioClip newTargetClip;

    void OnEnable()
    {
        GameEvents.OnDialsMatched += PlayDialsMatchedSound;
        GameEvents.OnWithinRange += PlayWithinRangeSound;
        GameEvents.OnNewTargets += PlayNewTargetSound;
    }

    void OnDisable()
    {
        GameEvents.OnDialsMatched -= PlayDialsMatchedSound;
        GameEvents.OnWithinRange -= PlayWithinRangeSound;
        GameEvents.OnNewTargets -= PlayNewTargetSound;
    }

    void PlayDialsMatchedSound() => PlayClip(dialsMatchedClip);
    void PlayWithinRangeSound() => PlayClip(withinRangeClip);
    void PlayNewTargetSound() => PlayClip(newTargetClip);

    void PlayClip(AudioClip clip)
    {
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }
}

