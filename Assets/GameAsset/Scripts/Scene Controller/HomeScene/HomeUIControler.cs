using System;
using System.Collections;
using System.Collections.Generic;
using Base.MessageSystem;
using Global;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Base.Audio;
using TMPro;
using Translation;

public class HomeUIControler : MonoBehaviour
{
    public ClockMonitorControler EnergyMonitorControler;
    public GameObject PopupOutOfEnergy;
    public RawImage currentVehicleRawImg;
    public Text nameVehicleText;
    [SerializeField] private TextMeshProUGUI durabilityText;
    [SerializeField] private TextMeshProUGUI efficiencyText;
    [SerializeField] private TextMeshProUGUI energyText;
    Vehicle _currentVehicle;

    private void Awake()
    {
        _currentVehicle = ClientData.Instance.ClientUser.clientVehicle.currentVehicle;
        LoadEnergyMonitor();
        LoadNameVehicle();
        LoadImageVehicle();

        durabilityText.text = $"Durability: \n{_currentVehicle.DurabilityPercent() * 100}" + "%";
        efficiencyText.text = $"Efficiency: \n{_currentVehicle.ModelStats().Efficiency}";
        string energyTxt = String.Empty;
        if (_currentVehicle.ModelStats().NftType == NFTType.Bicycle) energyTxt = "Stamina:";
        else if (_currentVehicle.ModelStats().NftType == NFTType.Shoes) energyTxt = "Stamina:";
        else if (_currentVehicle.ModelStats().NftType == NFTType.Car) energyTxt = "Gas:";

        energyText.text = $"{energyTxt} \n{_currentVehicle.EnergyPercent() * 100}" + "%";
    }
    void Start()
    {
        Translator.Translate("HomeScene");
        SoundManager.PlayMusic(ClientData.Instance.GetAudioClip(Audio.AudioType.Music, "music001"), 0.3f, true, true);
    }

    void LoadImageVehicle()
    {
        if (_currentVehicle != null)
        {
            currentVehicleRawImg.texture
                = ClientData.Instance.GetSpriteModelVehicle(_currentVehicle.ModelID).sprite.texture;
        }
    }

    void LoadNameVehicle()
    {
        if (_currentVehicle != null)
        {
            nameVehicleText.text = _currentVehicle.NameItem;
        }
    }

    void LoadEnergyMonitor()
    {
        if (_currentVehicle != null)
        {
            EnergyMonitorControler.Initialize(new float[] { 0f, 1f });
            EnergyMonitorControler.SetValue(_currentVehicle.EnergyPercent());
        }
    }


    #region Button Click Handler

    private void SceneTransferClick(Scenes sceneFrom, Scenes sceneTo)
    {
        SceneManager.LoadScene(sceneTo.ToString(), LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(sceneFrom.ToString());
    }

    public void ClickToDrivingScene()
    {
        if (_currentVehicle.IsOutOfEnergy())
        {
            PopupOutOfEnergy.SetActive(true);
        }
        else
        {
            SceneTransferClick(Scenes.HomeScene, Scenes.DrivingScene);
        }
    }

    public void OnButtonClick()
    {
        SoundManager.PlayUISound(ClientData.Instance.GetAudioClip(Audio.AudioType.UISound, "buttonClick01"));
    }

    #endregion
}
