using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AnkrSDK.Data;
using AnkrSDK.Utils;
using UnityEngine.UI;

public class SwitchChain : MonoBehaviour
{
    public Button switchButton;
    public GameObject signButton;

    void Awake()
    {
        switchButton.onClick.AddListener(switchChain);
    }

    void OnDestroy()
    {
        switchButton.onClick.RemoveListener(switchChain);
    }

    private void switchChain()
    {
        AnkrNetworkHelper.AddAndSwitchNetwork(NetworkName.BinanceSmartChain_TestNet);
        gameObject.SetActive(false);
        signButton.SetActive(true);
    }
}
