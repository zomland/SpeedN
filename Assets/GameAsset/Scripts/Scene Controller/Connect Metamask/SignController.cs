using System;
using System.Threading.Tasks;
using AnkrSDK.Core.Infrastructure;
using AnkrSDK.Examples.ERC20Example;
using AnkrSDK.Provider;
using AnkrSDK.Utils;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SignController : MonoBehaviour
{
    [Serializable]
	private class RequestPayload
	{
		public string Message;
		public string Signature;
	}

    [Serializable]
	private class RequestAnswer
	{
		public string Address;
	}

	private const string ProviderURL = "https://rinkeby.infura.io/v3/fe82f5256d5044ffa63d449cf6a0b107";
        
    public string _message = "Minh";
    public TextMeshProUGUI addressText;
	public Button signButton;
	public GameObject myWalletButton;
	public GameObject importButton;
	public GameObject importCoin;

    private IEthHandler _eth;

    string address="";
	string _signature ="";

    void Awake()
    {
        var ankrSDK = AnkrSDKFactory.GetAnkrSDKInstance(ProviderURL);
		_eth = ankrSDK.Eth;
		signButton.onClick.AddListener(sign);
    }

	void OnDestroy()
	{
		signButton.onClick.RemoveListener(sign);
	}

	public async void sign()
	{
		var address = await _eth.GetDefaultAccount();
		_signature = await _eth.Sign(_message, address);

		if(_signature !="")
		{
        	ClientData.Instance.ClientUser.address = address;
        	UpdateUILogs(ClientData.Instance.ClientUser.address);
			// myWalletButton.SetActive(true);
			// importButton.SetActive(true);
			importCoin.SetActive(true);
			gameObject.SetActive(false);
		}
	}
		
	private void UpdateUILogs(string log)
	{
		addressText.text = log;
	}
}
 