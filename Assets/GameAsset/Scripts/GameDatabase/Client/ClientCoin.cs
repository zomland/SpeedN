using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class ClientCoin
{
    public string nameCoin;
    public float amount;

    public ClientCoin(string _name, float _amount)
    {
        nameCoin = _name;
        amount = _amount;
    }
}
