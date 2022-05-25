using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientBicycle : ClientStaminaVehicle
{
    public ClientBicycle(BicycleAttribute bicycleAttribute)
    {
        Attrib = bicycleAttribute;
    }
}
