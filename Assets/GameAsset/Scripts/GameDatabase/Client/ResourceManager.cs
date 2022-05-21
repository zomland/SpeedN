using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Base;
using System;

public class ResourceManager : Singleton<ResourceManager>
{
    public SpeedNDefault speedNDefault;

    //Icon Sprite
    public SpriteIcon GetSpriteIcon(string name)
    {
        foreach(var child in speedNDefault.spriteIcons)
        {
            if(child.name == name) return child;
        }
        return null;
    }

    //Vehicle Sprite
    public SpriteVehicle GetSpriteVehicle(string name)
    {
        foreach(var child in speedNDefault.spriteVehicles)
        {
            if(child.name == name) return child;
        }
        return null;
    }
    
}

