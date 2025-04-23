using UnityEngine;
using FMODUnity;

[System.Serializable]
public class NamedAudioEvent
{
    public string name;
    public EventReference eventReference;
    public bool loop;
    
    // Call this to set and auto-name
    public void SetEventReference(EventReference reference)
    {
        eventReference = reference;

        if (!string.IsNullOrEmpty(reference.Path))
        {
            name = System.IO.Path.GetFileName(reference.Path);
        }
        else
        {
            name = "Unnamed Event";
        }

    }
}
