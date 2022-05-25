using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ClientShoes : ClientStaminaVehicle
{
    public ClientShoes(ShoesAttribute shoesAttribute)
    {
        Attrib = shoesAttribute;
    }
}
