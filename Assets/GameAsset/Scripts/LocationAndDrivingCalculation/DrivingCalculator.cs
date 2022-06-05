using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Base.Helper;

public class DrivingCalculator : MonoBehaviour
{
    const float earthRadius = 6371.0f;//Km
    const float timeStep = 0.5f;
    const float minDeltaDistance = 0.00083f * timeStep;
    const int secondsPerHour = 3600;
    const int secondsPerMin = 60;
    //=====================================================
    float deltaDistance = 0f;
    float distance = 0f;
    float timeDrove = 0f;
    float speed;
    float currentLongitude, currentLatitude;
    enum DrivingState
    {
        [StringValue("Start")] Start,
        [StringValue("Driving")] Driving,
        [StringValue("Pause")] Pause,
        [StringValue("Stop")] Stop
    }

    DrivingState drivingState = DrivingState.Stop;

    void Start()
    {
    }

    #region============================Haversine============================
    float DistanceUseHaversine(float longitude1, float latitude1, float longitude2, float latitude2)
    {

        float latRad1 = latitude1 * (Mathf.PI / 180f);
        float longRad1 = longitude1 * (Mathf.PI / 180f);
        float latRad2 = latitude2 * (Mathf.PI / 180f);
        float longRad2 = longitude2 * (Mathf.PI / 180f);
        float latDifference = latRad2 - latRad1;
        float longDifference = longRad2 - longRad1;
        float h = Mathf.Pow(Mathf.Sin(latDifference / 2), 2)
            + Mathf.Cos(latRad1) * Mathf.Cos(latRad2) * Mathf.Pow(Mathf.Sin(longDifference / 2), 2);
        return 2 * earthRadius * Mathf.Sin(Mathf.Sqrt(h));
    }
    #endregion ========================Haversine============================

    #region ===========================Calculate Distance===================
    void SetPosition()
    {
        currentLatitude = GPSController.Instance.Lat();
        currentLongitude = GPSController.Instance.Long();
    }
    float GetDeltaDistance()
    {
        if (drivingState == DrivingState.Start)
        {
            SetPosition();
            drivingState = DrivingState.Driving;
        }
        if (drivingState == DrivingState.Driving)
        {
            deltaDistance = DistanceUseHaversine(currentLongitude, currentLatitude
               , GPSController.Instance.Long(), GPSController.Instance.Lat());
            speed = deltaDistance / timeStep * secondsPerHour;
            SetPosition();
            UseEnergyVehicle(deltaDistance);
            if (deltaDistance > minDeltaDistance)
            {
                return deltaDistance;
            }
        }
        return 0f;
    }
    void CalculateDistance()
    {
        if (GPSController.Instance.isGPSAccessed())
            distance += GetDeltaDistance();
    }
    #endregion ========================Calculation Distance=================

    #region ===========================Calculate Energy & Coin==============
    void UseEnergyVehicle(float _deltaDistance)
    {
        ClientData.Instance.ClientUser.currentVehicle.UseEnergy(_deltaDistance);
    }
    #endregion ========================Calculate Energy & Coin==============

    #region ===========================Get result===========================
    public float Distance()
    {
        return distance;
    }
    public float Speed()
    {
        return speed;
    }
    public float GetTimeDrove()
    {
        return timeDrove;
    }
    public string timeDroveString()
    {
        int hour = (int)(timeDrove / secondsPerHour);
        int min = (int)(timeDrove % secondsPerHour / secondsPerMin);
        int second = (int)(timeDrove % secondsPerHour % secondsPerMin);
        return hour.ToString("00") + ":" + min.ToString("00") + ":" + second.ToString("00");
    }
    public float numCoin()
    {
        return ClientData.Instance.ClientUser.currentVehicle.Attrib.CoinPerKm * distance;
    }
    #endregion ========================Get result===========================

    #region ===========================Public Methods=======================
    public void PauseCalculate()
    {
        CancelInvoke("CalculateDistance");
        drivingState = DrivingState.Pause;
    }
    public void StartCalculate()
    {
        drivingState = DrivingState.Start;
        InvokeRepeating("CalculateDistance", 1f, timeStep);
    }
    public void ResetCalculator()
    {
        distance = 0;
        timeDrove = 0;
        drivingState = DrivingState.Stop;
    }
    #endregion ========================Public Methods=======================
    private void FixedUpdate()
    {
        if (drivingState == DrivingState.Driving)
            timeDrove += Time.fixedDeltaTime;
    }

    private void OnDestroy()
    {
        CancelInvoke("CalculateDistance");
    }
}
