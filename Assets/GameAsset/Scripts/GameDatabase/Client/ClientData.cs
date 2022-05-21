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
        
        //Test add Vehicle
        AddVehicle();
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



    private void AddVehicle()
    {
        int i =100;
        foreach(var child in speedNDefault.spriteVehicles)
        {
            var tmp = new ClientVehicle(child.name,i.ToString(),1f,1f,12);
            i+=10;
            clientUser.clientNFT.clientVehicles.Add(tmp);
        }
    }
}
