using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;
using System;

public enum GPS_LocationState
{
    Disabled,
    TimeOut,
    Failed,
    Enable
}
public class GPS_D : MonoBehaviour
{
    public float originalLatitude;
    public float originalLongitude;

    //public Text GPSstatus;
    public Text GPSdistance;


    public float EARTH_RADIUS = 6371;
    private double distance;
    private GPS_LocationState state;

    IEnumerator Start()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
            Permission.RequestUserPermission(Permission.CoarseLocation);
        }

        if (Input.location.isEnabledByUser)
        {

            Input.location.Start();
            //GPSstatus.text = "running";

            int maxWait = 50;
            while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
            {
                yield return new WaitForSeconds(1);
                maxWait--;
            }

            if (maxWait == 0)
            {
                //GPSstatus.text = "time out";
                state = GPS_LocationState.TimeOut;

            }
            else if (Input.location.status == LocationServiceStatus.Failed)
            {
                //GPSstatus.text = "Unable to determine device location";
                state = GPS_LocationState.Failed;
            }
            else
            {
                state = GPS_LocationState.Enable;
                originalLatitude = Input.location.lastData.latitude;
                originalLongitude = Input.location.lastData.longitude;

            }
        }
        else
        {
            //GPSstatus.text = "User has not  enabled GPS";
            yield break;
        }
    }
    //IEnumerator ApplicationPause(bool pauseState)
    //{
    //    if (pauseState)
    //    {
    //        Input.location.Stop();
    //        state = LocationState.Disabled;
    //    }
    //    else
    //    {
    //        Input.location.Start();
    //        GPSstatus.text = "running";

    //        // Wait until service initializes
    //        int maxWait = 50;
    //        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
    //        {
    //            yield return new WaitForSeconds(1);
    //            maxWait--;
    //        }

    //        // Service didn't initialize in 20 seconds
    //        if (maxWait == 0)
    //        {
    //            GPSstatus.text = "time out";
    //            state = LocationState.TimeOut;

    //        }
    //        else if (Input.location.status == LocationServiceStatus.Failed)
    //        {
    //            GPSstatus.text = "Unable to determine device location";
    //            state = LocationState.Failed;
    //        }
    //        else
    //        {
    //            state = LocationState.Enable;
    //            originalLatitude = Input.location.lastData.latitude;
    //            originalLongitude = Input.location.lastData.longitude;

    //        }
    //    }

    //}
    float Calc(ref float Lastlatitude, ref float Lastlongitude)
    {
        float newLatitude = Input.location.lastData.latitude;
        float newLongitude = Input.location.lastData.longitude;

        //GPSLat.text = "Lat: " + newLatitude.ToString();
        //GPSLon.text = "Long: " + newLongitude.ToString();

        float deltalLatitude = (newLatitude - Lastlatitude) * Mathf.Deg2Rad;
        float deltalLongitude = (newLongitude - Lastlongitude) * Mathf.Deg2Rad;

        float a = Mathf.Pow(Mathf.Sin(deltalLatitude / 2), 2) +
            Mathf.Cos(Lastlatitude * Mathf.Deg2Rad) * Mathf.Cos(newLatitude * Mathf.Deg2Rad)
            * Mathf.Pow(Mathf.Sin(deltalLongitude / 2), 2);
        Lastlatitude = newLatitude;
        Lastlongitude = newLongitude;

        float c = 2 * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt(1 - a));

        return EARTH_RADIUS * c;

    }

    void Update()
    {
        if (state == GPS_LocationState.Enable)
        {
            float Distance = Calc(ref originalLatitude, ref originalLongitude) * 1000f;
            if (Distance > 0f)
            {
                distance += Distance;

                //GPSdistance.text = "Distance: " + distance.ToString() + "m";

            }
        }
    }
}
