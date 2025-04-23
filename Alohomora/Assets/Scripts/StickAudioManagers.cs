using UnityEngine;
using FMODUnity;


public class StickAudioManagers : MonoBehaviour
{
    [SerializeField] private AudioEventConfig sceneAudioEventConfig;
    public GameObject thisStick;
    private TargetManager targetManager;
    private void OnEnable()
    {
        GameEvents.OnLockMove += PlayAudioLockMove;
        GameEvents.OnWithinRange += PlayAudioInRange;
    }

    private void OnDisable()
    {
        GameEvents.OnLockMove -= PlayAudioLockMove;
        GameEvents.OnWithinRange -= PlayAudioInRange;
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
    
    private void PlayAudioLockMove()
    {
        targetManager = FindObjectOfType<TargetManager>();
        if (targetManager != null && targetManager.actingStick == thisStick)
        {
            Debug.Log("This stick is the acting stick!");
            PlayEventByName("LockMovement");
        }
        else
        {
            Debug.Log("This stick is NOT the acting stick.");
        }
    }
    private void PlayAudioInRange()
    {
        targetManager = FindObjectOfType<TargetManager>();
        if (targetManager != null && targetManager.actingStick == thisStick)
        {
            Debug.Log("This stick is the acting stick!");
            PlayEventByName("StickLockOn");
        }
        else
        {
            Debug.Log("This stick is NOT the acting stick.");
        }
    }
    
}
