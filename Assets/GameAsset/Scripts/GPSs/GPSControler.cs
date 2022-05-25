using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GPSControler : MonoBehaviour
{
    public GameObject PopUpGPSWarning;
    //========================================================
    public bool isGPSAccessed = true;
    //========================================================
    float distance = 0;
    float oldLongitude, oldLatitude;
    float currentLongitude, currentLatitude;
    float timeCalculate = 0.3f;
    float timeCheckGPS = 1f;
    float timeTryToAccess = 10f;
    float speedCurrent;
    float speedAverage = 0;
    float speedAlmostRight = 0;
    float minSpeed = 3.6f;//Km
    float timeDrove = 0f;
    float numCoin = 0;
    const float earthRadius = 6376.5f;//Km
    //========================================================
    List<float> Speeds = new List<float>();
    //========================================================
    int timesGetSpeed = 5;
    enum DrivingState
    {
        Start, Driving, Pausing, Stop
    }
    DrivingState _drivingState = DrivingState.Start;
    Dictionary<string, DrivingState> StateDictionary = new Dictionary<string, DrivingState>()
    {
        {"Start",DrivingState.Start},
        {"Driving",DrivingState.Driving},
        {"Pausing",DrivingState.Pausing},
        {"Stop",DrivingState.Stop},
    };
    ClientVehicle _currentVehicle;
    const int secondsPerHour = 3600;
    const int secondsPerMin = 60;

    private void Start()
    {
        _currentVehicle = ClientData.Instance.ClientUser.currentVehicle;
        Input.location.Start(1f, 1f);
        StartCoroutine(WaitToLoadGPS());
        InvokeRepeating("CheckGPSAccessed", 0.5f, timeCheckGPS);
        InvokeRepeating("TryToAccessGPS", 0.6f, timeTryToAccess);
        InvokeRepeating("OnGPSState", 0.7f, timeCalculate);
    }

    public void SetState(string nameState)
    {
        _drivingState = StateDictionary[nameState];
    }

    #region GPS
    IEnumerator WaitToLoadGPS()
    {
        yield return new WaitForSeconds(3);
        InvokeRepeating("ShowPopupGPSWarning", 0.8f, 1f);
    }
    void CheckGPSAccessed()
    {
        if (Input.location.status == LocationServiceStatus.Running
            & Input.location.isEnabledByUser)
        {
            isGPSAccessed = true;
        }
        else
        {
            isGPSAccessed = false;
        }
    }
    void TryToAccessGPS()
    {
        if (!isGPSAccessed)
        {
            Input.location.Start(1f, 1f);
        }
    }
    void ShowPopupGPSWarning()
    {
        PopUpGPSWarning.SetActive(!isGPSAccessed);
    }
    #endregion

    #region OnGPSState
    void OnGPSState()
    {
        if (isGPSAccessed)
        {
            UpdateGPSData();
            CalculateDistance();
        }
        else
        {
            ShowPopupGPSWarning();
        }
    }
    void UpdateGPSData()
    {
        LocationInfo locationInfo = Input.location.lastData;
        currentLatitude = locationInfo.latitude;
        currentLongitude = locationInfo.longitude;
        if (_drivingState == DrivingState.Start && !_currentVehicle.IsOutOfEnergy())
        {
            oldLatitude = currentLatitude;
            oldLongitude = currentLongitude;
            _drivingState = DrivingState.Driving;
        }
    }
    void CalculateDistance()
    {
        if (_drivingState == DrivingState.Driving | _drivingState == DrivingState.Pausing)
        {
            float deltaDistance = DistanceFrom2Locations(oldLongitude, oldLatitude,
            currentLongitude, currentLatitude);
            oldLatitude = currentLatitude;
            oldLongitude = currentLongitude;

            ProcessSpeeds(deltaDistance);

            if (Speeds.Count == timesGetSpeed)
            {
                if (speedAlmostRight > minSpeed & _drivingState == DrivingState.Driving)
                {
                    deltaDistance = speedAlmostRight * timesGetSpeed * timeCalculate;
                    distance += deltaDistance;
                    UseEnergy(deltaDistance);
                    CalculateNumCoin();
                }
                Speeds.Clear();
            }
        }
    }

    public float DistanceFrom2Locations(float longitude, float latitude,
            float otherLongitude, float otherLatitude)
    {
        // Haversine
        float d1 = latitude * (Mathf.PI / 180f);
        float num1 = longitude * (Mathf.PI / 180f);
        float d2 = otherLatitude * (Mathf.PI / 180f);
        float num2 = otherLongitude * (Mathf.PI / 180f) - num1;
        float d3 = Mathf.Pow(Mathf.Sin((d2 - d1) / 2f), 2) +
                   Mathf.Cos(d1) * Mathf.Cos(d2) * Mathf.Pow(Mathf.Sin(num2 / 2.0f), 2);
        double result = earthRadius *
            (2.0f * Mathf.Atan2(Mathf.Sqrt(d3), Mathf.Sqrt(1.0f - d3)));
        return (float)result;
    }
    #endregion

    #region Process Speed
    void ProcessSpeeds(float deltaDistance)
    {
        GetSpeeds(deltaDistance);
        if (Speeds.Count == timesGetSpeed)
        {
            GetSpeedAverage();
            GetSpeedAlmostRight();
        }
    }
    void GetSpeeds(float deltaDistance)
    {
        speedCurrent = deltaDistance / timeCalculate * secondsPerHour;
        Speeds.Add(speedCurrent);
    }

    void GetSpeedAverage()
    {
        speedAverage = 0f;
        foreach (float speed in Speeds)
        {
            speedAverage += speed;
        }
        speedAverage = speedAverage / timesGetSpeed;
    }

    void GetSpeedAlmostRight()
    {
        float DifferenceFromAverage = 0;
        speedAlmostRight = Speeds[0];
        for (int index = 1; index < Speeds.Count; index++)
        {
            if (DifferenceFromAverage > Mathf.Abs(Speeds[index] - speedAverage))
            {
                DifferenceFromAverage = Mathf.Abs(Speeds[index] - speedAverage);
                speedAlmostRight = Speeds[index];
            }
        }
    }
    #endregion

    #region  Data Record
    void timeDroveRun()
    {
        timeDrove += Time.fixedDeltaTime;
    }

    void CalculateNumCoin()
    {
        numCoin = distance * _currentVehicle.Attrib.CoinPerMeter;
    }

    void UseEnergy(float _deltaDistance)
    {
        _currentVehicle.UseEnergy(_deltaDistance * 1000f);
    }
    #endregion

    #region  Get data
    public float GetDistance()
    {
        return distance;
    }
    public float GetSpeed()
    {
        return speedAlmostRight;
    }
    public float GetNumCoin()
    {
        return numCoin;
    }
    public float GetTimeDrove()
    {
        return timeDrove;
    }
    public string GetTimeDroveString()
    {
        int hour = (int)timeDrove / secondsPerHour;
        int min = (int)timeDrove % secondsPerHour / secondsPerMin;
        int second = (int)timeDrove % secondsPerHour % secondsPerMin;
        return hour.ToString("00") + ":" + min.ToString("00") + ":" + second.ToString("00");
    }
    #endregion

    void FixedUpdate()
    {
        timeDroveRun();
    }
}