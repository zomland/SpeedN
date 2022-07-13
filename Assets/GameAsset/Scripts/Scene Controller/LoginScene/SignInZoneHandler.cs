using System;
using System.Collections;
using System.Collections.Generic;
using FirebaseHandler;
using UnityEngine;

namespace UIHandler
{
    public class SignInZoneHandler : MonoBehaviour
    {
        [SerializeField] private GameObject googleSignInButton;
        [SerializeField] private GameObject appleSignInInButton;

        private void Start()
        {
            bool isAndroid = Application.platform == RuntimePlatform.Android;
            googleSignInButton.SetActive(isAndroid);
            appleSignInInButton.SetActive(!isAndroid);
        }
    }
}

