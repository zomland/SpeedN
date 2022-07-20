using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Global;
using FirebaseHandler;

public class MyItemSceneUI_2Controller : MonoBehaviour
{
    [Header("Vehicle")]
    public Image spriteVehicle;
    public TextMeshProUGUI vehicleIDText;
    public TextMeshProUGUI nameText;
    public ClockMonitorControler EnergyMonitorControler;
    public ClockMonitorControler DurabilityMonitorControler;

    [Header("Buttons")]
    public Button ButtonFillUp;
    public Button ButtonRepair;

    Vehicle Vehicle;

    [HideInInspector]
    public float FeeEnergy;
    [HideInInspector]
    public float FeeRepair;
    [HideInInspector]

    public void DisplayUI(Vehicle _Vehicle)
    {
        Vehicle = _Vehicle;
        spriteVehicle.sprite = ClientData.Instance.GetSpriteModelVehicle(Vehicle.ModelID).sprite;
        vehicleIDText.text = Vehicle.ItemID;
        nameText.text = Vehicle.NameItem;
        EnergyMonitorControler.SetValue(Vehicle.EnergyPercent());
        DurabilityMonitorControler.SetValue(Vehicle.DurabilityPercent());
        CheckButtonFillAndRepair();
    }

    public void CheckButtonFillAndRepair()
    {
        // if (Vehicle.EnergyPercent() == 1) ButtonFillUp.interactable = false;
        // else ButtonFillUp.interactable = true;
        // if (Vehicle.DurabilityPercent() == 1) ButtonRepair.interactable = false;
        // else ButtonRepair.interactable = true;
    }

    public void ResetMonitors()
    {
        EnergyMonitorControler.ResetMonitor();
        DurabilityMonitorControler.ResetMonitor();
    }
}
