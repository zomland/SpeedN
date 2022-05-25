using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ClientMotobike : ClientGasVehicle
{
    public ClientMotobike(MotobikeAttribute motobikeAttribute)
    {
        Attrib = motobikeAttribute;
    }
}
