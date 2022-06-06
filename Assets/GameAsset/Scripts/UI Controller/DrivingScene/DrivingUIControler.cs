using System.Collections;
using System.Collections.Generic;
using Base.MessageSystem;
using Global;
using UnityEngine;
using UnityEngine.UI;
#if PLATFORM_ANDROID
using UnityEngine.Android;
#endif

public class DrivingUIControler : MonoBehaviour
{
    [Header("DrivingUIElement")]
    public Text textVehicleName;
    public GameObject CountDownScene;
    public Text textCountdown;
    public Text textDistance;
    public Text textNumCoin;
    public Text textLimitSpeed;
    public Text textTimeDrove;
    public Image imgGPSStatus;
    public ClockMonitorControler EnergyMonitorControler;
    public ClockMonitorControler SpeedMonitorControler;
    public GameObject PopupGPSWarning;
    public Button buttonGPSStart;
    //==============================================================
    [Header("MovingRecord")]
    public MovingRecordDetailControler _movingRecordDetailControler;
    bool isShowRecord = false;
    //==============================================================
    [Header("Calculator")]
    [SerializeField] private DrivingCalculator drivingCalculator;
    //==============================================================

    ClientVehicle _currentVehicle;
    const float minDistanceToRecord = 0.005f;//Km
    const float minTimeDroveToRecord = 10f;//second


    void Start()
    {
        _currentVehicle = ClientData.Instance.ClientUser.currentVehicle;
        float[] vehicleLimitSpeed = _currentVehicle.Attrib.LimitSpeed;
        textLimitSpeed.text = vehicleLimitSpeed[0].ToString() + " - " + vehicleLimitSpeed[1].ToString() + " Km/h";
        textVehicleName.text = _currentVehicle.Attrib.Name;
        EnergyMonitorControler.Initialize(new float[] { 0f, 1f });
        SpeedMonitorControler.Initialize(new float[] { 0f, vehicleLimitSpeed[1] * 2 + 1000f });
        StartCoroutine(CountDown());
        InvokeRepeating("ShowPopupGPSWarning", 2f, 3f);
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
        drivingCalculator.StartCalculate();
    }

    #region ================================Show Driving=============================
    void ShowOnDriving()
    {
        if (!isShowRecord)
        {
            if (GPSController.Instance.isGPSAccessed()) imgGPSStatus.color = Color.green;
            else imgGPSStatus.color = Color.red;
            _currentVehicle = ClientData.Instance.ClientUser.currentVehicle;
            textTimeDrove.text = drivingCalculator.timeDroveString();
            textDistance.text = drivingCalculator.Distance().ToString("0.0");
            textNumCoin.text = drivingCalculator.numCoin().ToString("0.0");
            EnergyMonitorControler.SetValue(_currentVehicle.EnergyPercent());
            SpeedMonitorControler.SetValue(drivingCalculator.Speed());
        }
    }
    void ShowPopupGPSWarning()
    {
        PopupGPSWarning.SetActive(!GPSController.Instance.isGPSAccessed());
    }

    #endregion================================Show Driving=============================

    #region ==================================Show Record==============================
    public void ShowMovingRecord()
    {
        if (!isShowRecord)
        {
            if (drivingCalculator.GetTimeDrove() < minTimeDroveToRecord
                | drivingCalculator.Distance() < minDistanceToRecord)
            {
                isShowRecord = true;
                BackToHome();
            }
            else
            {
                drivingCalculator.PauseCalculate();
                _movingRecordDetailControler.CreateMovingRecord(drivingCalculator.numCoin()
                    , _currentVehicle.Attrib.Name, drivingCalculator.Distance()
                        , drivingCalculator.timeDroveString(), drivingCalculator.GetTimeDrove());

                _movingRecordDetailControler.DisplayMovingRecord();
                isShowRecord = true;
            }
        }
    }
    void CheckShowMovingRecord()
    {
        if (_currentVehicle.IsOutOfEnergy() && !isShowRecord)
        {
            ShowMovingRecord();
        }
    }
    #endregion ==================================Show Record==============================

    #region =====================================Button click=============================
    public void GotoWallet()
    {
        Debug.LogWarning("Go to wallet");
        //Messenger.RaiseMessage(Message.LoadScene,Scenes.MyWallet,Scenes.HomeScene);
    }
    public void PauseDriving()
    {
        drivingCalculator.PauseCalculate();
        Debug.LogWarning("Pausing");
    }
    public void StartDriving()
    {
        drivingCalculator.StartCalculate();
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
    public void StartGPSAgain()
    {
        GPSController.Instance.isGPSEnableByUser();
        PopupGPSWarning.SetActive(false);
        InvokeRepeating("ShowPopupGPSWarning", 10f, 3f);
    }

    #endregion =====================================Button click=============================

    void Update()
    {
        ShowOnDriving();
        CheckShowMovingRecord();
        Debug.Log(GPSController.Instance.isGPSAccessed());
        #if UNITY_ANDROID
        buttonGPSStart.interactable = GPSController.Instance.isGPSEnableByUser()
            & Permission.HasUserAuthorizedPermission(Permission.FineLocation);
        #elif UNITY_IOS
        #endif
        Debug.Log(ClientData.Instance.ClientUser.currentVehicle.Attrib.Gas);
    }
}
