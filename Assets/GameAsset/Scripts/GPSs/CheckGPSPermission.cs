using System;
using System.Collections;
using System.Collections.Generic;
using Base.Helper;
using UnityEngine;
using UnityEngine.Android;

namespace Runtime.Controller
{
    public class CheckGPSPermission : BaseMono
    {
        private string _gpsProblem = String.Empty;
        private bool _isGPSAccessed = true;
        private float _timeCheckGPS = 1f;
        private float _timeTryToAccess = 13f;
        private float _timeOnGPSState = 1f;

        private void Start()
        {
            StartCoroutine(GpsPermission());
        }

        private IEnumerator GpsPermission()
        {
            if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
            {
                Permission.RequestUserPermissions(new []{Permission.CoarseLocation, Permission.FineLocation});
            }

            while (true)
            {
                if (Input.location.isEnabledByUser)
                {
                    
                }
                else
                {
                    
                }

                yield return new WaitForSeconds(_timeCheckGPS);
            }
        }
    }
}

