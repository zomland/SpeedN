using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Base.Audio;

[CreateAssetMenu(fileName = "SpeedN Default")]
public class SpeedNDefault : ScriptableObject
{
    [Header("SpriteModelVehicles")]
    public List<SpriteModelVehicle> spriteModelVehicles;
    
    [Header("AudioClip")]
    public List<musicAudioClip> musicAudioClips;
    public List<soundAudioClip> soundAudioClips;
    public List<UISoundAudioClip> UISoundAudioClips;
}
