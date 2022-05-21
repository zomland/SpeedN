using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="SpeedN Default")]
public class SpeedNDefault : ScriptableObject
{

    [Header("Icon")]
    public List<SpriteIcon> spriteIcons;
    [Header("Vehicle")]
    public List<SpriteVehicle> spriteVehicles;
}
