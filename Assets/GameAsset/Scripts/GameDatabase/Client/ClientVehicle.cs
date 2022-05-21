using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class ClientVehicle : BaseNFT
{
    public string name;
    public string vehicleID;
    public int level;
    public int levelMax;
    public float endurancePercent;
    public float energy;
    public float energyMax;
    public float energyPerMeter;
    public float coinPerMeter;
    public float exp;
    public float[] expToUpLevels;

    public event Action<string,string> VehicleCallback;


    public ClientVehicle(string _name, string _vehicleID, float _energyPerMeter, float _coinPerMeter
        , int _levelMax)
    {
        name = _name;
        vehicleID = _vehicleID;
        levelMax = _levelMax;
        expToUpLevels = new float[_levelMax + 1];
        CreateExpToUpLevels();
        level = 0;
        exp = 0;
        endurancePercent = 1f;
        energyMax = 100f;
        energy = energyMax;
        energyPerMeter = _energyPerMeter;
        coinPerMeter = _coinPerMeter;
        Debug.Log("hi");
    }

    void CreateExpToUpLevels()
    {
        float coefficient = 10;
        for (int index = 0; index < expToUpLevels.Length; index++)
        {
            expToUpLevels[index] = coefficient * index;
        }
    }

    public void DecreaseEndurance(float decreasePercent)
    {
        Debug.Log("Decrease Endurance");
        endurancePercent-= decreasePercent;
    }

    public void UseEnergy(float meter)
    {
        if(energy>0)
        {
            energy -= energyPerMeter * meter;
        }
        else
        {
            Debug.Log("het nang luong");
        }
        
    }

    public float energyPercent()
    {
        return energy / energyMax;
    }

    public void Repair()
    {
        Debug.Log("Vehicle " + name + " repair");
        endurancePercent = 1f;
    }

    public void fillUpEnergy()
    {
        Debug.Log("Vehicle " + name + "fill up energy");
        energy = 1f;
    }

    public void pushExp(float expAmount)
    {

        if (level < levelMax)
        {
            exp += expAmount;
            if (exp >= expToUpLevels[level])
            {
                level++;
                Debug.Log("Vehicle" + name + " up to level" + level);
            }
        }
        else
        {
            Debug.Log("Vehicle" + name + " level current is maximum");
        }
    }

    void OnVehicleProcess(string nameMethod, string message)
    {

    }

}
