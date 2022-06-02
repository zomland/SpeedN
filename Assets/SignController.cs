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

    private const string URL = "http://root@eth-01.dccn.ankr.com:8080/account/verification/address";
        
    public string _message = "Minh";
    public TextMeshProUGUI addressText;
	public Button signButton;

    private IEthHandler _eth;

    string address;
    string _signature;

    void Awake()
    {
        var ankrSDK = AnkrSDKFactory.GetAnkrSDKInstance(ERC20ContractInformation.HttpProviderURL);
		_eth = ankrSDK.Eth;
		signButton.onClick.AddListener(sign);
    }

	public async void sign()
	{
		var address = await _eth.GetDefaultAccount();
		_signature = await _eth.Sign(_message, address);
		var tmp = await SendSignature(_signature);
        ClientData.Instance.ClientUser.address = tmp;
        UpdateUILogs(ClientData.Instance.ClientUser.address);
	}

	private async Task<string> SendSignature(string signature)
	{
		var requestPayload = new RequestPayload
		{
			Message = _message,
			Signature = signature
		};

		var payload = JsonConvert.SerializeObject(requestPayload);

		var result = await AnkrSDKHelper.GetUnityWebRequestFromJSON(URL, payload).SendWebRequest();
		var data = JsonConvert.DeserializeObject<RequestAnswer>(result.downloadHandler.text);
		return data.Address;
	}
		
	private void UpdateUILogs(string log)
	{
		addressText.text = log;
	}
}
