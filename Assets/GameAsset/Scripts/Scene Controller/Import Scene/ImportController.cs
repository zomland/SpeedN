using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AnkrSDK.Core.Infrastructure;
using AnkrSDK.Provider;
using AnkrSDK;

public class ImportController : MonoBehaviour
{
    [Header("Button")]
    public Button importCoinButton;
    public Button importNFTButton;

    [Header("Popup")]
    public GameObject coinPopUp;
    public GameObject NFTPopUp;
    void Start()
    {
        importCoinButton.onClick.AddListener(ImportCoin);
        importNFTButton.onClick.AddListener(ImportNFT);
    } 

    void OnDestroy()
    {
        importCoinButton.onClick.RemoveListener(ImportCoin);
        importNFTButton.onClick.RemoveListener(ImportNFT);
    }

    private void ImportCoin()
    {
        coinPopUp.SetActive(true);
    }
    private void ImportNFT()
    {
        NFTPopUp.SetActive(true);
    }
}
