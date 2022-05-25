using System.Collections;
using System.Collections.Generic;
using Base.MessageSystem;
using Global;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DrivingUIControler : MonoBehaviour
{
    [Header("Driving")]
    public Text textVehicleName;
    public GameObject CountDownScene;
    public Text textCountdown;
    public Text textDistanceDriving;
    public Text textNumCoin;
    public Image imgGPSStatus;
    public ClockMonitorControler EnergyMonitorControler;
    public ClockMonitorControler SpeedMonitorControler;
    //--------------------------------

    [Header("MovingRecord")]
    public MovingRecordControler _movingRecordControler;
    public bool isShowRecord = false;
    //-------------------------

    public GPSControler _GPSControler;
    ClientVehicle _currentVehicle;
    const float minDistanceToRecord = 0.005f;//Km
    const float minTimeDroveToRecord = 10f;//second


    void Start()
    {
        _currentVehicle = ClientData.Instance.ClientUser.currentVehicle;
        textVehicleName.text = _currentVehicle.Attrib.Name;
        EnergyMonitorControler.Initialize(new float[] { 0f, 1f });
        SpeedMonitorControler.Initialize(_currentVehicle.Attrib.LimitSpeed);
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

    #region Show Driving
    void ShowOnDriving()
    {
        if (_GPSControler.isGPSAccessed) imgGPSStatus.color = Color.green;
        else imgGPSStatus.color = Color.red;
        ShowDistance();
        textNumCoin.text = _GPSControler.GetNumCoin().ToString("0.0");
        EnergyMonitorControler.SetValue(_currentVehicle.EnergyPercent());
        SpeedMonitorControler.SetValue(_GPSControler.GetSpeed());
    }
    void ShowDistance()
    {
        textDistanceDriving.text = _GPSControler.GetDistance().ToString("0.00");

    }
    #endregion

    #region Show Record
    public void ShowMovingRecord()
    {
        if (!isShowRecord)
        {
            if (_GPSControler.GetTimeDrove() < minTimeDroveToRecord 
                & _GPSControler.GetDistance() > minDistanceToRecord)
            {
                BackToHome();
            }
            else
            {
                _movingRecordControler.CreateMovingRecord(_GPSControler.GetNumCoin()
                    , _currentVehicle.Attrib.Name, _GPSControler.GetDistance()
                        , _GPSControler.GetTimeDroveString());

                _movingRecordControler.DisplayMovingRecord();
                isShowRecord = true;
            }
        }
    }
    void CheckShowMovingRecord()
    {
        if (_currentVehicle.IsOutOfEnergy() && !isShowRecord)
        {
            ShowMovingRecord();
            _GPSControler.SetState("Stop");
        }
    }
    #endregion

    #region Button click
    public void GotoWallet()
    {
        Debug.LogWarning("Go to wallet");
        //Messenger.RaiseMessage(Message.LoadScene,Scenes.MyWallet,Scenes.HomeScene);
    }
    public void PauseDriving()
    {
        _GPSControler.SetState("Pausing");
        Debug.LogWarning("Pausing");
    }
    public void ResumeDriving()
    {
        _GPSControler.SetState("Driving");
        Debug.LogWarning("Resume");
    }
    public void Reload()
    {
        Debug.LogWarning("Reload");
    }
    public void BackToHome()
    {
        Messenger.RaiseMessage(Message.LoadScene, Scenes.HomeScene, Scenes.DrivingScene);
    }
    #endregion

    void Update()
    {
        ShowOnDriving();
        CheckShowMovingRecord();
        Debug.Log("Distance: " + _GPSControler.GetDistance().ToString());
    }
}
