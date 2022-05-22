using System.Collections;
using System.Collections.Generic;
using Base.MessageSystem;
using Global;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DrivingUIControler : MonoBehaviour
{
    public ClockMonitorControler EnergyMonitorControler;
    public RawImage currentVehicleRawImg;
    public Text nameVehicleText;
    public Text textMessage;
    public GameObject CountDownScene;
    public GameObject MovingRecordDetailScene;
    public Text textCountdown;
    public GPS_H GPSControler;

    [Range(0f, 1f)]
    public float valueEnergy;
    public bool isStart = true;
    float timeCountdown = 3f;


    void Start()
    {
        LoadNameVehicle();
        LoadImageVehicle();
        LoadMessage();
        StartCoroutine(CountDown());
    }

    IEnumerator CountDown()
    {
        CountDownScene.SetActive(true);
        textCountdown.text = "3";
        yield return new WaitForSeconds(1f);
        textCountdown.text = "2";
        yield return new WaitForSeconds(1f);
        textCountdown.text = "1";
        yield return new WaitForSeconds(1f);
        textCountdown.text = "Start";
        yield return new WaitForSeconds(0.3f);
        CountDownScene.SetActive(false);
    }

    public void BackToHome()
    {
        Messenger.RaiseMessage(Message.LoadScene, Scenes.HomeScene, Scenes.DrivingScene);
    }

    void LoadNameVehicle()
    {
        nameVehicleText.text = ClientData.Instance.ClientUser.currentVehicle.name;
    }

    void LoadMessage()
    {
        textMessage.text="Using vehicle" + ClientData.Instance.ClientUser.currentVehicle.name;
    }

    void LoadImageVehicle()
    {
        currentVehicleRawImg.texture
        = ClientData.Instance.GetSpriteVehicle
        (ClientData.Instance.ClientUser.currentVehicle.name).sprite.texture;
    }

    void LoadDataMovingRecordDetail()
    {
        GPSControler.LoadDataForRecord();
    }

    public void ShowMovingRecordDetail()
    {
        LoadDataMovingRecordDetail();
        SaveRecord();
        MovingRecordDetailScene.SetActive(true);
    }

    void SaveRecord()
    {
        Debug.LogWarning("Save record");
    }

    void DecreaseEnergy()
    {
        ClientData.Instance.ClientUser.currentVehicle.UseEnergy(GPSControler.GetDistance());
    }


    void UpdateEnergyMonitor()
    {
        DecreaseEnergy();
        EnergyMonitorControler.SetValueShow(ClientData.Instance.ClientUser.currentVehicle.energyPercent());
    }

    public void GotoWallet()
    {
        Debug.LogWarning("Go to wallet");
    }

    public void PauseDriving()
    {
        GPSControler.isPausing = true;
        Debug.LogWarning("Pausing");
    }

    public void ResumeDriving()
    {
        GPSControler.isPausing = false;
        Debug.LogWarning("Resume");
    }

    public void Reload()
    {
        Debug.LogWarning("Reload");
    }

    void Update()
    {
        UpdateEnergyMonitor();
    }
}
