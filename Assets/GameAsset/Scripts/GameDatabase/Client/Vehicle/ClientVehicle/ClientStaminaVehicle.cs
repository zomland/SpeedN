using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ClientStaminaVehicle : ClientVehicle
{
    public ClientStaminaVehicle() { }

    public override bool IsOutOfEnergy()
    {
        return Attrib.Stamina <= 0f;
    }
    public override float EnergyPercent()
    {
        return Attrib.Stamina / Attrib.StaminaMax;
    }
    public override void UseEnergy(float Km)
    {
        if (Attrib.Stamina > 0)
        {
            Attrib.Stamina -= Attrib.StaminaPesKm * Km;
            if (Attrib.Stamina < 0) Attrib.Stamina = 0f;
        }
        else
        {
            Debug.Log("Out of energy");
        }
    }
    public override void FillUpEnergy()
    {
        Attrib.Stamina = Attrib.StaminaMax;
    }
}
