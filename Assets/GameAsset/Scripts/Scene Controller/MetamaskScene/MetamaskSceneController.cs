using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class MetamaskSceneController : MonoBehaviour
{
    [Header("Info")]
    public string address;
    public float amountEnerzi;

    [Header("UI Default")]
    public TextMeshProUGUI addressText;
    public TextMeshProUGUI amountText;

    [Header("UI Popup Confirm")]
    public TextMeshProUGUI addressTextPopup;
    public TextMeshProUGUI amountImportText;
    public TextMeshProUGUI alertText;
    public GameObject successPopup;
    public GameObject rejectPopup;

    string alert;
    float amountImport;
    

    void Start()
    {
        amountImport =  PlayerPrefs.GetFloat("AmountImport", 0);

        addressText.text =  address;
        amountText.text = amountEnerzi.ToString();
        addressTextPopup.text =  address;
        amountImportText.text =  amountImport.ToString();

        CheckAlert();
    }

    private void CheckAlert()
    {
        if(amountImport > amountEnerzi)
        {
            alert ="NOT ENOUGH BALANCE";
        }
        alertText.text =  alert;
    }

    public void ConfirmButton()
    {
        if(alert=="") return;
        successPopup.SetActive(true);
        ClientData.Instance.ClientUser.numCoin += amountImport;
    }

    public void RejectButton()
    {
        rejectPopup.SetActive(true);
    }
}

     
