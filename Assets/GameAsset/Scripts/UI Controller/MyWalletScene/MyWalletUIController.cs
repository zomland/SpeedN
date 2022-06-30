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
        userNameText.text = ClientData.Instance.ClientUser.userName;
        amountCoinText.text = ClientData.Instance.ClientCoin.Coins[0].amount.ToString();
        typeCoinText.text = ClientData.Instance.ClientCoin.Coins[0].nameCoin;
        DisplayAddress();
    }

    private void DisplayAddress(){
        string address =  ClientData.Instance.ClientUser.address;
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

    public void DisplayCoin(Coin coin){
        amountCoinText.text = coin.amount.ToString();
        typeCoinText.text = coin.nameCoin;
    }
}
