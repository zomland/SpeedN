using Base;
using System;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientData : Singleton<ClientData>{
    [NonSerialized] public ClientUser clientUser;
    public SpeedNDefault speedNDefault;
    public ResourceManager resourceManager;

    void Awake(){
        clientUser = new ClientUser(speedNDefault);
        resourceManager = GetComponent<ResourceManager>();
    }
}
