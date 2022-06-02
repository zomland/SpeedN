using Cysharp.Threading.Tasks;
using System;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

	public class LoginController     : MonoBehaviour
	{
		public Button _loginButton;
		public TextMeshProUGUI address;
        public GameObject switchChain;
	#if !UNITY_WEBGL || UNITY_EDITOR
		[SerializeField] private AnkrSDK.WalletConnectSharp.Unity.WalletConnect _walletConnect;
	#endif

    bool isConnected = false;

		private void OnEnable()
		{
		#if UNITY_WEBGL && !UNITY_EDITOR
			try
			{
				_loginButton.gameObject.SetActive(false);
			}
			catch (Exception)
			{
				
			}
		#else
			TrySubscribeToWalletEvents().Forget();
		#endif
		}


	#if UNITY_WEBGL && !UNITY_EDITOR
	#elif !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IOS)
		private UnityAction GetLoginAction()
		{
			return _walletConnect.OpenDeepLink;
		}
#else
		private UnityAction GetLoginAction()
		{
	        return () => Debug.Log($"Trying to open ");
		}
	#endif

	#if !UNITY_WEBGL || UNITY_EDITOR
		private void OnDisable()
		{
			UnsubscribeFromTransportEvents();
			_walletConnect.ConnectionStarted -= OnConnectionStarted;
		}

		private async UniTask TrySubscribeToWalletEvents()
		{
			await UniTask.WaitUntil(() => _walletConnect.Session != null);

			_loginButton.onClick.AddListener(GetLoginAction());

			SubscribeOnTransportEvents();
		}

		private void SubscribeOnTransportEvents()
		{
			UpdateSceneState();
			var walletConnectUnitySession = _walletConnect.Session;
			UpdateLoginButtonState(this, walletConnectUnitySession);

			_walletConnect.ConnectedEvent.AddListener(UpdateSceneState);

			walletConnectUnitySession.OnTransportConnect += UpdateLoginButtonState;
			walletConnectUnitySession.OnTransportDisconnect += UpdateLoginButtonState;
			walletConnectUnitySession.OnTransportOpen += UpdateLoginButtonState;
			walletConnectUnitySession.OnSessionDisconnect += OnSessionDisconnect;
		}

		private void OnSessionDisconnect(object sender, EventArgs e)
		{
			UnsubscribeFromTransportEvents();
			UpdateLoginButtonState(this, _walletConnect.Session);
			_walletConnect.ConnectionStarted += OnConnectionStarted;
		}

		private void OnConnectionStarted()
		{
			_walletConnect.ConnectionStarted -= OnConnectionStarted;

			TrySubscribeToWalletEvents().Forget();
		}

		private void UnsubscribeFromTransportEvents()
		{
			_loginButton.onClick.RemoveAllListeners();
			_walletConnect.ConnectedEvent.RemoveListener(UpdateSceneState);

			var walletConnectSession = _walletConnect.Session;
			if (walletConnectSession == null)
			{
				return;
			}

			walletConnectSession.OnTransportConnect -= UpdateLoginButtonState;
			walletConnectSession.OnTransportDisconnect -= UpdateLoginButtonState;
			walletConnectSession.OnTransportOpen -= UpdateLoginButtonState;
			walletConnectSession.OnSessionDisconnect -= OnSessionDisconnect;
		}

		private void UpdateLoginButtonState(object sender, AnkrSDK.WalletConnectSharp.Core.WalletConnectProtocol e)
		{
			UpdateSceneState();
			_loginButton.interactable = e.TransportConnected;
		}

		private void UpdateSceneState(AnkrSDK.WalletConnectSharp.Core.Models.WCSessionData _ = null)
		{
			var walletConnectUnitySession = _walletConnect.Session;
			if (walletConnectUnitySession == null)
			{
				return;
			}
			var activeSessionConnected = walletConnectUnitySession.Connected;
            if(activeSessionConnected && isConnected == false)
            {
                switchChain.SetActive(activeSessionConnected);
			    _loginButton.gameObject.SetActive(!activeSessionConnected);
                isConnected = true;
            }        
		}
	#endif
	}