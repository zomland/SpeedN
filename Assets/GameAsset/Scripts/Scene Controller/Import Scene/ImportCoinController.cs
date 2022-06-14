using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AnkrSDK.Core.Infrastructure;
using AnkrSDK.Provider;
using AnkrSDK;
using System;
using  System.Numerics;
using TMPro;
using Cysharp.Threading.Tasks;
using Nethereum.ABI.FunctionEncoding.Attributes;
using AnkrSDK.Data.ContractMessages.ERC721;
using  AnkrSDK.EventListenerExample;

public class ImportCoinController : MonoBehaviour
{
    private const string ContractAddress= "0xBEfd84C542F8e80645F6441b9572e2584c2cBD01";

    private const string ABI = "[{\"inputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"constructor\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":true,\"internalType\":\"address\",\"name\":\"owner\",\"type\":\"address\"},{\"indexed\":true,\"internalType\":\"address\",\"name\":\"spender\",\"type\":\"address\"},{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"value\",\"type\":\"uint256\"}],\"name\":\"Approval\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":true,\"internalType\":\"address\",\"name\":\"from\",\"type\":\"address\"},{\"indexed\":true,\"internalType\":\"address\",\"name\":\"to\",\"type\":\"address\"},{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"value\",\"type\":\"uint256\"}],\"name\":\"Transfer\",\"type\":\"event\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"owner\",\"type\":\"address\"},{\"internalType\":\"address\",\"name\":\"spender\",\"type\":\"address\"}],\"name\":\"allowance\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"spender\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"amount\",\"type\":\"uint256\"}],\"name\":\"approve\",\"outputs\":[{\"internalType\":\"bool\",\"name\":\"\",\"type\":\"bool\"}],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"account\",\"type\":\"address\"}],\"name\":\"balanceOf\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"amount\",\"type\":\"uint256\"}],\"name\":\"burn\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"account\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"amount\",\"type\":\"uint256\"}],\"name\":\"burnFrom\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"decimals\",\"outputs\":[{\"internalType\":\"uint8\",\"name\":\"\",\"type\":\"uint8\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"spender\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"subtractedValue\",\"type\":\"uint256\"}],\"name\":\"decreaseAllowance\",\"outputs\":[{\"internalType\":\"bool\",\"name\":\"\",\"type\":\"bool\"}],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"spender\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"addedValue\",\"type\":\"uint256\"}],\"name\":\"increaseAllowance\",\"outputs\":[{\"internalType\":\"bool\",\"name\":\"\",\"type\":\"bool\"}],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"name\",\"outputs\":[{\"internalType\":\"string\",\"name\":\"\",\"type\":\"string\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"symbol\",\"outputs\":[{\"internalType\":\"string\",\"name\":\"\",\"type\":\"string\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"totalSupply\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"recipient\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"amount\",\"type\":\"uint256\"}],\"name\":\"transfer\",\"outputs\":[{\"internalType\":\"bool\",\"name\":\"\",\"type\":\"bool\"}],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"sender\",\"type\":\"address\"},{\"internalType\":\"address\",\"name\":\"recipient\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"amount\",\"type\":\"uint256\"}],\"name\":\"transferFrom\",\"outputs\":[{\"internalType\":\"bool\",\"name\":\"\",\"type\":\"bool\"}],\"stateMutability\":\"nonpayable\",\"type\":\"function\"}]";
    private const string ProviderURL = "https://rinkeby.infura.io/v3/fe82f5256d5044ffa63d449cf6a0b107";

    string addressRecieve ="0x16E13eCAc9d039c3A47bD15D62bA5b4a4A1049d5";
    public Button confirmButton;
    public Button confirmButton2;

    public TextMeshProUGUI amount;


    private IContract _contract;
	private IAnkrSDK _ankrSDKWrapper;
    private IEthHandler _eth;
    private BigInteger balance;
    [Parameter("uint256", "_decimals", 1)] public BigInteger decimals { get; set; } 
    [Parameter("uint256", "_value", 2)] public BigInteger value { get; set; } 

    void Awake()
    {
        confirmButton.onClick.AddListener(Confirm);
        confirmButton2.onClick.AddListener(Confirm2);
        
    } 

    void Start()
    {
        decimals = 1000000000000000000;
        SetSmartContract();
    }

    void OnDestroy()
    {
        confirmButton.onClick.RemoveListener(Confirm);
        confirmButton2.onClick.RemoveListener(Confirm2);
    }

    public async void SetSmartContract()
    {
        _ankrSDKWrapper = AnkrSDKFactory.GetAnkrSDKInstance(ProviderURL);
		_contract = _ankrSDKWrapper.GetContract(ContractAddress, ABI);

        _eth = _ankrSDKWrapper.Eth;
        var address =  await _eth.GetDefaultAccount();
        amount.text =  address;
    }

    public async UniTaskVoid GetBalance()
		{
			var balanceOfMessage = new BalanceOfMessage
			{
				Owner = await _eth.GetDefaultAccount()
			};
			balance = await _contract.GetData<BalanceOfMessage, BigInteger>(balanceOfMessage);
			amount.text = balance.ToString() ;
		}

    public  async void Transfer1 ()
    {
        var gasEstimation = await _contract.EstimateGas("transfer", new object[]{addressRecieve , decimals * 100 });
        amount.text =  gasEstimation.ToString();
        var receipt =  await _contract.CallMethod("transfer", new object[]{addressRecieve , decimals * 100 },"250000",gasEstimation.ToString());
        var trx = await _eth.GetTransaction(receipt);

        amount.text = trx.Nonce.ToString();
    }

    public  async void Transfer2 ()
    {
        var receipt =  await _contract.CallMethod("transfer", new object[]{addressRecieve , decimals * 100 },"250000","300000");
        var trx = await _eth.GetTransaction(receipt);

        amount.text = trx.Nonce.ToString();
    }
    public  UniTask<string> transferToken()
    {
        return _contract.CallMethod("transfer", new object[]{addressRecieve , decimals * 100 }).AsUniTask();
    }   

    public void Confirm()
    {
       Transfer1();
    }

    public void Confirm2()
    {
        Transfer2();
    }
}
