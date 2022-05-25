using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MyItemSceneUI_2Controller : MonoBehaviour
{
    public Image spriteVehicle;
    public TextMeshProUGUI vehicleIDText;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI energyText;

    public TextMeshProUGUI levelText;

    ClientVehicle vehicle;

    public void DisplayUI(ClientVehicle vehicle)
    {
        this.vehicle = vehicle;
        spriteVehicle.sprite = ClientData.Instance.GetSpriteVehicle(vehicle.Attrib.Name).sprite;
        vehicleIDText.text = vehicle.Attrib.ID;
        nameText.text = vehicle.Attrib.Name;
        energyText.text = (vehicle.EnergyPercent()*100).ToString() + "%";
        levelText.text =  vehicle.Attrib.Level.ToString();
    }
}
