using System;
using System.Collections;
using System.Collections.Generic;
using Base.Helper;
using UnityEngine;
using UnityEngine.Android;
using Base;

public class GPSController : Singleton<GPSController>
{
    void Start()
    {
        InvokeRepeating("CheckPermissionGPS", 0f, 10f);
        StartGPS();
    }

    void CheckPermissionGPS()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.CoarseLocation) |
            !Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermissions(new string[] { Permission.CoarseLocation, Permission.FineLocation });
        }
    }

    public void StartGPS()
    {
        StartCoroutine(LoadLocationService());
    }

    IEnumerator LoadLocationService()
    {
        int maxWait = 10;
        while (Permission.HasUserAuthorizedPermission(Permission.FineLocation) && maxWait > 0)
        {
            Debug.Log("permit " + Permission.HasUserAuthorizedPermission(Permission.FineLocation));
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // Check if the user has location service enabled.
        if (!Input.location.isEnabledByUser)
            yield break;

        // Starts the location service.
        Input.location.Start(1f, 1f);
        Debug.Log("StartGPS");

        // Waits until the location service initializes
        maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // If the service didn't initialize in 20 seconds this cancels location service use.
        if (maxWait < 1)
        {
            print("Timed out");
            yield break;
        }

        // If the connection failed this cancels location service use.
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            print("Unable to determine device location");
            yield break;
        }
    }

    #region ===============================Get Location Info===============================================
    public float Lat()
    {
        return Input.location.lastData.latitude;
    }
    public float Long()
    {
        return Input.location.lastData.longitude;
    }
    public float horizontalAccuracy()
    {
        return Input.location.lastData.horizontalAccuracy;
    }
    public float verticalAccuracy()
    {
        return Input.location.lastData.verticalAccuracy;
    }
    public float Altitude()
    {
        return Input.location.lastData.verticalAccuracy;
    }
    public string GPSStatus()
    {
        if (Input.location.status == LocationServiceStatus.Stopped) return "Stopped";
        if (Input.location.status == LocationServiceStatus.Initializing) return "Initializing";
        if (Input.location.isEnabledByUser & Input.location.status == LocationServiceStatus.Running)
            return "Enable & Running";
        if (Input.location.status == LocationServiceStatus.Running) return "Only Running";
        if (Input.location.status == LocationServiceStatus.Failed) return "Failed";
        return "Exception";
    }
    public bool isGPSAccessed()
    {
        return Input.location.isEnabledByUser & Input.location.status == LocationServiceStatus.Running;
    }
    public bool isGPSEnableByUser()
    {
        return Input.location.isEnabledByUser;
    }
    #endregion ============================Location Info===================================================

    #region ===============================Public Methods==================================================
    public void StopLocationService()
    {
        Input.location.Stop();
    }
    #endregion =============================Public Methods=================================================

}


