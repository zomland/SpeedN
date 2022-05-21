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

        spriteVehicle.sprite = ClientData.Instance.GetSpriteVehicle(vehicle.name).sprite;
        vehicleIDText.text = vehicle.vehicleID;
        nameText.text = vehicle.name;
        energyText.text = (vehicle.energy / vehicle.energyMax).ToString() + "%";
        levelText.text =  vehicle.level.ToString();
    }
}
