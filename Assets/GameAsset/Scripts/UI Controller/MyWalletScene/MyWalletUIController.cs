using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MyWalletUIController : MonoBehaviour
{
    public TextMeshProUGUI amountCoinText;
    public TextMeshProUGUI typeCoinText;
    public TextMeshProUGUI addressText;
    public TextMeshProUGUI userNameText;

    void Start(){
        userNameText.text = ClientData.Instance.clientUser.userName;
        amountCoinText.text = ClientData.Instance.clientUser.clientCoins[0].amount.ToString();
        typeCoinText.text = ClientData.Instance.clientUser.clientCoins[0].nameCoin;

        DisplayAddress();
    }

    private void DisplayAddress(){
        string address =  ClientData.Instance.clientUser.address;
        string s="";
        for (int i =0;i< address.Length ;i++)
        {
            if( i==5) break;    
            s+= address[i];
        }
        s+="...";
        for(int i = address.Length -1 ;i >=0; i--)
        {
            if(i == address.Length -4) break;
            s+= address[i];
        }

        addressText.text = s;
    }

    public void DisplayCoin(ClientCoin coin){
        amountCoinText.text = coin.amount.ToString();
        typeCoinText.text = coin.nameCoin;
    }
}
