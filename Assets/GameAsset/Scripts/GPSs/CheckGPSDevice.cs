using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class CheckGPSDevice : MonoBehaviour
{
    public Text textGPSProblem;
    public GameObject PopUpGPSWarning;
    //=====================================
    string gpsProblem = "";
    //=====================================
    bool isGPSAccessed = true;
    //=====================================
    float timeCheckGPS = 1f;
    float timeTryToAccess = 13f;
    float timeOnGPSState = 1f;
    //=====================================

    void Start()
    {
        StartCoroutine(TurnOnGPSWhenStart());
        InvokeRepeating("CheckGPSAccessed", 0f, timeCheckGPS);
        InvokeRepeating("OnGPSState", 0.1f, timeOnGPSState);
        InvokeRepeating("TryToAccessGPS", 0.2f, timeTryToAccess);
    }

    void CheckGPSAccessed()
    {
        //Debug.Log("Permission: "+Permission.HasUserAuthorizedPermission(Permission.CoarseLocation));
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

    IEnumerator TurnOnGPSWhenStart()
    {
        Input.location.Start(1f, 1f);
        yield return new WaitForSeconds(1);
        Input.location.Start(1f, 1f);
        yield break;
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
        gpsProblem = "GPS access success";
        ShowGPSProblem();
    }

    void OnGPSNotAccessed()
    {
        gpsProblem = "Please turn on and permit access to GPS";
        ShowGPSProblem();
    }

    void ShowGPSProblem()
    {
        textGPSProblem.text = gpsProblem;
        PopUpGPSWarning.SetActive(!isGPSAccessed);
    }

    void TryToAccessGPS()
    {
        if (!isGPSAccessed)
        {
            Input.location.Start(1f, 1f);
        }
    }
}
