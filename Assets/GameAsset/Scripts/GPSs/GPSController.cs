using System;
using System.Collections;
using System.Collections.Generic;
using Base.Helper;
using UnityEngine;
using UnityEngine.Android;

public class GPSController : MonoBehaviour
{
    private const float EarthRadius = 6376.5e3f; // Meters
    #region Private members

    private ClientVehicle _clientVehicle;
    private PermissionCallbacks _permissionCallbacks;
    private DrivingState _drivingState = DrivingState.Stop;
    
    private double _oldLongitude, _oldLatitude;
    private double _currentLongitude, _currentLatitude;

    #endregion

    #region Private field

    [SerializeField] private GameObject popupGpsWarning;

    #endregion

    #region Properties

    public bool IsLocationReady { get; private set; }
    public bool LocationGrantedAndroid { get; private set; }
    
    public double Distance { get; private set; }

    public DrivingState DrivingState
    {
        get => _drivingState;
        set => _drivingState = value;
    }

    #endregion
    

    // -------------------------- Unity Event function ------------------------------------

    #region Unity Event function
    private void Start()
    {
        _clientVehicle = ClientData.Instance.ClientUser.currentVehicle;
        _permissionCallbacks = new PermissionCallbacks();
        _permissionCallbacks.PermissionDenied += PermissionDenied;
        _permissionCallbacks.PermissionDeniedAndDontAskAgain += PermissionDeniedAndDontAsk;
        _permissionCallbacks.PermissionGranted += PermissionGranted;

        CheckGpsAccess();
    }

    private void Update()
    {
        
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            CheckGpsAccess();
        }
        else
        {
            _drivingState = DrivingState.Pausing;
        }
    }

    private void OnDestroy()
    {
        _permissionCallbacks.PermissionDenied -= PermissionDenied;
        _permissionCallbacks.PermissionDeniedAndDontAskAgain -= PermissionDeniedAndDontAsk;
        _permissionCallbacks.PermissionGranted -= PermissionGranted;
    }

    #endregion
    
    // ------------------------- End region -------------------------------------------------

    // -------------------------------------- Private function ---------------------------------------

    #region Private function

    private void CheckGpsAccess()
    {
#if PLATFORM_ANDROID

        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermissions(new []{Permission.FineLocation, Permission.CoarseLocation}, 
                _permissionCallbacks);
        }
        else
        {
            LocationGrantedAndroid = true;
            IsLocationReady = NativeGPSPlugin.StartLocation();
            
            if (IsLocationReady)
            {
                _oldLatitude = NativeGPSPlugin.GetLatitude();
                _oldLongitude = NativeGPSPlugin.GetLongitude();
                _drivingState = DrivingState.Driving;

                BaseInterval.RunAtEveryFrame(this, 0, 2f, CalculateDistance);
            }
        }
#elif PLATFORM_IOS
        
        
#endif
    }

    private void CalculateDistance(IntervalConfig config)
    {
        if (!IsLocationReady)
        {
            Debug.Log("Is Location Ready: " + IsLocationReady);
            return;
        }

        if (_drivingState == DrivingState.Driving || _drivingState == DrivingState.Pausing)
        {
            _currentLatitude = NativeGPSPlugin.GetLatitude();
            _currentLongitude = NativeGPSPlugin.GetLongitude();
            double deltaDistance = Haversine(_oldLongitude, _oldLatitude, 
                _currentLongitude, _currentLatitude);
            _oldLatitude = NativeGPSPlugin.GetLatitude();
            _oldLongitude = NativeGPSPlugin.GetLongitude();
            Debug.Log("delta distance: " + deltaDistance);

            Distance += deltaDistance;
        }
        else if (_drivingState == DrivingState.Stop)
        {
            config.isCancel = true;
            Debug.Log("Go to here: ");
        }
    }

    private double Haversine(double oldLongitude, double oldLatitude, double longitude, double latitude)
    {
        double d1 = latitude * (Mathf.PI / 180f);
        double num1 = longitude * (Mathf.PI / 180f);
        double d2 = oldLatitude * (Mathf.PI / 180f);
        double num2 = oldLongitude * (Mathf.PI / 180f) - num1;
        double d3 = Math.Pow(Math.Sin((d2 - d1) / 2f), 2) +
                    Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0f), 2);
        double result = EarthRadius *
                        (2.0f * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0f - d3)));
        return result; // In meters
    }

    private void PermissionDenied(string message)
    {
        ShowPopupGpsWarning();
    }

    private void PermissionDeniedAndDontAsk(string message)
    {
        ShowPopupGpsWarning();
    }

    private void PermissionGranted(string message)
    {
        LocationGrantedAndroid = true;
        IsLocationReady = NativeGPSPlugin.StartLocation();

        if (IsLocationReady)
        {
            _oldLatitude = NativeGPSPlugin.GetLatitude();
            _oldLongitude = NativeGPSPlugin.GetLongitude();
            _drivingState = DrivingState.Driving;
        }
    }
    
    private void ShowPopupGpsWarning()
    {
        popupGpsWarning.SetActive(!IsLocationReady);
    }

    #endregion
    
    /* ---------------------------------------------------------------------------------------------------
    --------------------------------------------------------------------------------------------------- */
    
    // --------------------------------------- Public function -------------------------------------------

    #region Public function

    public float GetSpeed()
    {
        if (IsLocationReady)
        {
            return NativeGPSPlugin.GetSpeed();
        }

        return 0;
    }

    public float GetDistance()
    {
        return (float) Distance; // In meters
    }

    #endregion
    // -----------------------------------------------------------------------------------------------------
}

public enum DrivingState
{
    Start, Driving, Pausing, Stop
}
