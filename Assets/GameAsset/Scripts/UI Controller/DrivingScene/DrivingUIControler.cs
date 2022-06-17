using System.Collections;
using System.Collections.Generic;
using Base.MessageSystem;
using Global;
using UnityEngine;
using FirebaseHandler;
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
    public Text textUnitDistance;
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

    VehicleController _currentVehicleController;
    const float minDistanceToRecord = 0.005f;//Km
    const float minTimeDroveToRecord = 10f;//second


    void Start()
    {
        _currentVehicleController = ClientData.Instance.ClientUser.currentVehicleController;
        float[] vehicleLimitSpeed = new float[] { _currentVehicleController.data.minSpeed, _currentVehicleController.data.maxSpeed };
        textLimitSpeed.text = vehicleLimitSpeed[0].ToString() + " - " + vehicleLimitSpeed[1].ToString() + " Km/h";
        textVehicleName.text = _currentVehicleController.data.name;
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
            _currentVehicleController = ClientData.Instance.ClientUser.currentVehicleController;
            textTimeDrove.text = drivingCalculator.timeDroveString();
            ShowDistanceAndNumCoin();
            EnergyMonitorControler.SetValue(_currentVehicleController.EnergyPercent());
            SpeedMonitorControler.SetValue(drivingCalculator.Speed());
        }
    }
    void ShowDistanceAndNumCoin()
    {
        float _distance = drivingCalculator.Distance();
        float _numCoin = drivingCalculator.numCoin();
        if (_distance < 1)
        {
            textUnitDistance.text = "m";
            textDistance.text = (_distance * 1000).ToString("0.0");
        }
        else
        {
            textUnitDistance.text = "km";
            textDistance.text = _distance.ToString("0.0");
        }
        if (_numCoin < 0.01f & _numCoin > 0f)
        {
            textNumCoin.text = _numCoin.ToString("0.0000");
        }
        else
        {
            textNumCoin.text = _numCoin.ToString("0.00");
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
                    , _currentVehicleController.data.name, drivingCalculator.Distance()
                        , drivingCalculator.timeDroveString(), drivingCalculator.GetTimeDrove());

                _movingRecordDetailControler.DisplayMovingRecord();
                ClientData.Instance.ClientUser.EarnCoin("BNB", drivingCalculator.numCoin());
                FirebaseApi.Instance.PostUser(callbackPostUserAfterDriving).Forget();
                isShowRecord = true;
            }
        }
    }
    void CheckShowMovingRecord()
    {
        if (_currentVehicleController.IsOutOfEnergy() && !isShowRecord)
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

    void callbackPostUserAfterDriving(string a, string b, int c)
    {

    }

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
    }
}
