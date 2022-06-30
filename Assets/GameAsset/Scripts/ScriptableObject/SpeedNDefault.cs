using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Base.Audio;

[CreateAssetMenu(fileName = "SpeedN Default")]
public class SpeedNDefault : ScriptableObject
{

    [Header("Icon")]
    public List<SpriteIcon> spriteIcons;

    [Header("spriteModelVehicles")]
    public List<SpriteModelVehicle> spriteModelVehicles;

    [Header("ModelVehicle")]
    public List<ModelVehicleBaseStats> modelVehicleBaseStats;

    [Header("AudioClip")]
    public List<musicAudioClip> musicAudioClips;
    public List<soundAudioClip> soundAudioClips;
    public List<UISoundAudioClip> UISoundAudioClips;
}
