using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;

[System.Serializable]
public class ClientCoin
{
    public List<Coin> Coins = new List<Coin>();
    public ClientCoin(){ }


    public void CreateFromLocal(SpeedNDefault speedNDefault, int numCoin)
    {
        for (int i = 0; i < speedNDefault.spriteIcons.Count; i++)
        {
            var coin = new Coin(speedNDefault.spriteIcons[i].spriteID, numCoin);
            Coins.Add(coin);
        }
    }

    public float GetAmountCoin(string nameCoin)
    {
        foreach (var child in Coins)
        {
            if (child.nameCoin == nameCoin) return child.amount;
        }
        return 0;
    }
    public void SwapCoin(string send, string get, float amountSend, float amountGet)
    {
        foreach (var child in Coins)
        {
            bool isSend = false;
            if (child.nameCoin == send)
            {
                if (child.amount > amountSend)
                {
                    child.amount -= amountSend;
                    isSend = true;
                }
                else
                {
                    Debug.Log("SwapCoin: " + send + "is not enough");
                }
            }
            else if (child.nameCoin == get & isSend)
            {
                child.amount += amountGet;
            }
        }
    }
    public void EarnCoin(string nameCoin, float amount)
    {
        Debug.Log("Earn Coin: " + nameCoin + " | " + amount);
    }

    public void UseCoin(string nameCoin, float amount)
    {
        Debug.Log("Use Coin: " + nameCoin + " | " + amount);
    }
    public bool isEnoughCoin(string nameCoin, float amount)
    {
        return true;
    }
}

[System.Serializable]
public class Coin
{
    public string nameCoin;
    public float amount;

    public Coin()
    {
        nameCoin = "null";
    }

    public Coin(string _name, float _amount)
    {
        nameCoin = _name;
        amount = _amount;
    }

    public string GetStringJsonData()
    {
        return JsonConvert.SerializeObject(this);
    }
}
