using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ClientGasVehicle : ClientVehicle
{
    public ClientGasVehicle() { }
    public override bool IsOutOfEnergy()
    {
        return Attrib.Gas <= 0f;
    }
    public override float EnergyPercent()
    {

        return Attrib.Gas / Attrib.GasMax;
    }
    public override void UseEnergy(float Km)
    {
        if (Attrib.Gas > 0f)
        {
            Attrib.Gas -= Attrib.GasPerKm * Km;
            if (Attrib.Gas < 0) Attrib.Gas = 0f;
        }
        else
        {
            Debug.Log("Out of energy");
        }
    }
    public override void FillUpEnergy()
    {
        Attrib.Gas = Attrib.GasMax;
    }
}
