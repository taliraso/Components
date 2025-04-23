using UnityEngine;
using FMODUnity;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioEventConfig sceneAudioEventConfig;
    public GameObject LeftStick;
    public GameObject RightStick;

    private void Start()
    {
        PlayEventByName ("Music");
    }
    
    private void OnEnable()
    {
        GameEvents.EnterLockMechanic += PlayAudioEnterLockMechanic;
        GameEvents.ExitLockMechanic += PlayAudioExitLockMechanic;
        GameEvents.OnDialsMatched += PlayAudioDialsMatched;
        GameEvents.OnNewTargets += PlayAudioNewTargets;
        GameEvents.Unlock += PlayAudioUnlock;
        GameEvents.OnStickMove += PlayAudioStickMove;

    }

    private void OnDisable()
    {
        GameEvents.EnterLockMechanic -= PlayAudioEnterLockMechanic;
        GameEvents.ExitLockMechanic -= PlayAudioExitLockMechanic;
        GameEvents.OnDialsMatched -= PlayAudioDialsMatched;
        GameEvents.OnNewTargets -= PlayAudioNewTargets;
        GameEvents.Unlock -= PlayAudioUnlock;
        GameEvents.OnStickMove -= PlayAudioStickMove;
    }

    private void PlayAudioEnterLockMechanic()
    {
        PlayEventByName("EnterLockMechanic");
    }
    
    private void PlayAudioExitLockMechanic()
    {
        PlayEventByName("ExitLockMechanic");
    }
    
    private void PlayAudioDialsMatched()
    {
        PlayEventByName("HoldTimerStart");
    }
    
    private void PlayAudioNewTargets()
    {
        PlayEventByName("NewTargets");
    }
    private void PlayAudioUnlock()
    {
        PlayEventByName("Unlock");
    }
    
    public void PlayEventByName(string eventName)
    {
        var namedEvent = sceneAudioEventConfig.audioEvents.Find(e => e.name == eventName);
        if (namedEvent != null)
        {
            RuntimeManager.PlayOneShot(namedEvent.eventReference);
        }
        else
        {
            Debug.LogWarning($"Audio Event '{eventName}' not found in config!");
        }
    }
    public void PlayEventFromGameObject(string eventName, GameObject source)
    {
        var namedEvent = sceneAudioEventConfig.audioEvents.Find(e => e.name == eventName);
        if (namedEvent != null)
        {
            RuntimeManager.PlayOneShotAttached(namedEvent.eventReference, source);
        }
        else
        {
            Debug.LogWarning($"Audio Event '{eventName}' not found in config!");
        }
    }
    
    private void PlayAudioStickMove(StickType stick)
    {
        Debug.Log($"Stick moved: {stick}");

        if (stick == StickType.Left)
        {
            // Do something for the left stick
            PlayEventFromGameObject("StickInit", LeftStick);
        }
        else
        {
            // Do something for the right stick
            PlayEventFromGameObject("StickInit", RightStick);
        }
    }


}

