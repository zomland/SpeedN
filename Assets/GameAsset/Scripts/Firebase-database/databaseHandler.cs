using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FirebaseHandler;
using System;
using Global;

public class databaseHandler : MonoBehaviour
{

    FirebaseDatabaseHandler _databaseHandler = new FirebaseDatabaseHandler();
    [NonSerialized] ClientUser user;
    public SpeedNDefault speedNDefault;

    private void Awake()
    {
        _databaseHandler.InitializeDatabase();
        _databaseHandler.SetValueChangedCallBack(OnValueChanged);
    }
    void Start()
    {

    }
    void OnFirebaseDatabaseHandling(string nameProcedure, string message, int errorID)
    {
        string detailError = "";
        switch (errorID)
        {
            case (int)DatabaseErrorID.None:
                detailError = "None error";
                break;
            default:
                break;
        }
        Debug.LogWarning(nameProcedure + ": " + message + ": " + detailError);
    }

    void OnValueChanged(string nameProcedure, string message, int errorID)
    {
        Debug.LogWarning(nameProcedure + ": " + message);
    }
}
