using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MyWalletItem : MonoBehaviour
{ 
    public Image icon;
    public TextMeshProUGUI nameCoinText;
    public TextMeshProUGUI amountCoinText;

    string nameCoin ;
    float amount;

    MyWalletUIController myWalletUIController;

    void Start(){
        myWalletUIController = FindObjectOfType<MyWalletUIController>();
    }

    public void SetProperties(Sprite icon , string nameCoin , float amountCoin)
    {
        this.nameCoin = nameCoin;
        this.amount = amountCoin;
        this.icon.sprite = icon;
        this.nameCoinText.text = nameCoin;
        this.amountCoinText.text = amountCoin.ToString();
    }

    public void suadi(){
        Debug.Log("1");
    }

    public void OnClickItemCoin(){
        myWalletUIController.DisplayCoin(amount , nameCoin);
    }
}
