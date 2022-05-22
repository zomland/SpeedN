using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GPS_H : MonoBehaviour
{
    public ClockMonitorControler SpeedMonitorControler;
    public Text timeDrivenText;
    public Text textDistanceOnDriving;
    public Text textDistanceOnRecord;
    public Text textNumCoinTextOnDriving;
    public Text textNumCoinTextOnRecord;
    public GameObject PopUpGPSWarning;
    public Image GPSStatus;
    public float maxSpeed;
    public bool isPausing = false;
    //========================================================
    bool isCalculatingDistance = true;
    bool isStart = true;
    bool isGPSAccessed = true;
    //========================================================
    float distance = 0;
    float oldLongitude, oldLatitude;
    float currentLongitude, currentLatitude;
    float minVelocity = 1f;
    float deltaDistance;
    float timeStep = 1f;
    float velocityAverage = 0;
    float velocityAlmostRight = 0;
    const float earthRadius = 6376500.0f;
    float timeCheckGPS = 1f;
    float timeTryToAccess = 10f;
    //float deltaClockSpeed=0.01f;
    //========================================================
    List<float> velocities = new List<float>();
    //========================================================
    int timeGetVelocity = 5;
    float timeDriven = 0f;
    float numCoin = 0;



    private void Start()
    {
        SpeedMonitorControler.SetMaxValue(maxSpeed);
        Input.location.Start(1f, 1f);
        StartCoroutine(WaitToLoadGPS());
        InvokeRepeating("CheckGPSAccessed", 0.5f, timeCheckGPS);
        InvokeRepeating("TryToAccessGPS", 0.6f, timeTryToAccess);
        InvokeRepeating("OnGPSState", 0.7f, timeStep);
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

    IEnumerator WaitToLoadGPS()
    {
        yield return new WaitForSeconds(3);
        InvokeRepeating("ShowPopupGPSWarning", 0.8f, 1f);
    }

    void ShowPopupGPSWarning()
    {
        PopUpGPSWarning.SetActive(!isGPSAccessed);
    }

    void OnGPSState()
    {
        if (isGPSAccessed)
        {
            OnGPSAccessed();
        }
        else
        {
            OnGPSNotAccessed();
        }
    }

    void OnGPSAccessed()
    {
        UpdateGPSData();
        CalculateDistance();
        CalculateNumCoin();
        ShowNumCoinOnDriving();
        GPSStatus.color = Color.green;
    }

    void OnGPSNotAccessed()
    {
        GPSStatus.color = Color.red;
        Debug.LogWarning("gps not access"); ;
    }

    void UpdateGPSData()
    {
        LocationInfo locationInfo = Input.location.lastData;
        currentLatitude = locationInfo.latitude;
        currentLongitude = locationInfo.longitude;
        if (isStart)
        {
            oldLatitude = currentLatitude;
            oldLongitude = currentLongitude;
            isStart = false;
            textDistanceOnDriving.text = distance.ToString("0.0");
            Debug.Log("Distance: " + distance);
        }
    }

    void CalculateDistance()
    {
        if (isCalculatingDistance)
        {
            deltaDistance = DistanceFrom2Locations(oldLongitude, oldLatitude,
            currentLongitude, currentLatitude);
            //Debug.Log("deltaD: " + deltaDistance);
            oldLatitude = currentLatitude;
            oldLongitude = currentLongitude;

            ProcessVelocities(deltaDistance);

            if (velocities.Count == timeGetVelocity)
            {
                if (velocityAlmostRight > minVelocity & !isPausing)
                {
                    distance += velocityAlmostRight * timeStep * timeGetVelocity;
                }
                velocities.Clear();

            }
            ShowOnUI();
        }
    }

    void ShowOnUI()
    {
        if (isCalculatingDistance)
        {
            textDistanceOnDriving.text = distance.ToString("0.0");
            UpdateSpeedMonitor();
        }
    }

    void UpdateSpeedMonitor()
    {
        SpeedMonitorControler.SetValueShow(velocityAlmostRight / maxSpeed);
    }

    void ProcessVelocities(float deltaDistance)
    {
        GetVelocities(deltaDistance);
        if (velocities.Count == timeGetVelocity)
        {
            GetVelocityAverage();
            GetVelocityAlmostRight();
        }
    }

    void GetVelocities(float deltaDistance)
    {
        velocities.Add(deltaDistance / timeStep);
    }

    void GetVelocityAverage()
    {
        velocityAverage = 0f;
        foreach (float velocity in velocities)
        {
            velocityAverage += velocity;
        }
        velocityAverage = velocityAverage / timeGetVelocity;
    }

    void GetVelocityAlmostRight()
    {
        float DifferenceFromAverage = 0;
        foreach (float velocity in velocities)
        {
            if (DifferenceFromAverage == 0)
            {
                DifferenceFromAverage = Mathf.Abs(velocity - velocityAverage);
                velocityAlmostRight = velocity;
            }
            else
            {
                if (DifferenceFromAverage > Mathf.Abs(velocity - velocityAverage))
                {
                    DifferenceFromAverage = Mathf.Abs(velocity - velocityAverage);
                    velocityAlmostRight = velocity;
                }
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
        double mdistance = earthRadius *
            (2.0f * Mathf.Atan2(Mathf.Sqrt(d3), Mathf.Sqrt(1.0f - d3)));
        return (float) mdistance;
    }

    public void ResetDistance()
    {
        distance = 0f;
        isCalculatingDistance = false;
        velocities.Clear();
    }

    public void PauseCalculating()
    {
        Debug.Log("Pausing calculation");
    }

    void timeDrivenRun()
    {
        timeDriven += Time.fixedDeltaTime;
    }

    void timeDrivenShow()
    {
        const int secondsPerHour = 3600;
        const int secondsPerMin = 60;
        int hour = (int)timeDriven / secondsPerHour;
        int min = (int)timeDriven % secondsPerHour / secondsPerMin;
        int second = (int)timeDriven % secondsPerHour % secondsPerMin;
        timeDrivenText.text = hour.ToString("00") + ":" + min.ToString("00") + ":" + second.ToString("00");
    }

    void CalculateNumCoin()
    {
        numCoin = distance * ClientData.Instance.ClientUser.currentVehicle.coinPerMeter;
    }

    void ShowNumCoinOnDriving()
    {
        textNumCoinTextOnDriving.text = numCoin.ToString("0.0");
    }

    void ShowNumCoinOnRecord()
    {
        textNumCoinTextOnRecord.text = numCoin.ToString("0.0");
    }

    void ShowDistanceOnRecord()
    {
        textDistanceOnRecord.text = "+" + distance.ToString("0.0");
    }

    void DecreaseEnergy()
    {

    }

    public void LoadDataForRecord()
    {
        ShowNumCoinOnRecord();
        ShowDistanceOnRecord();
    }

    public float GetDistance()
    {
        return distance;
    }

    void FixedUpdate()
    {
        timeDrivenRun();
    }

    void Update()
    {

    }
}