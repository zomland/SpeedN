using Base;
using System;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientData : Singleton<ClientData>{
    [NonSerialized] public ClientUser clientUser;
    public SpeedNDefault speedNDefault;

    void Awake(){
        clientUser = new ClientUser(speedNDefault);
    }

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
