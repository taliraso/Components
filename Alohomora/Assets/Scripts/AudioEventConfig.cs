using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewAudioEventSet", menuName = "Audio/Named Audio Event Set")]
public class AudioEventConfig : ScriptableObject
{
    public List<NamedAudioEvent> audioEvents = new List<NamedAudioEvent>();
}
