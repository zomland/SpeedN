using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ImportSceneUIController : MonoBehaviour
{
    public string nameCoin;
    public TextMeshProUGUI amountCoinWallet;
    public TextMeshProUGUI amountCoinGame;
    public TextMeshProUGUI nameCoinText;
    public TMP_InputField input;

    string amount;
    ImportController importController;

    void Start()
    {
        importController = GetComponent<ImportController>();
        nameCoinText.text = nameCoin;
    }

    void Update()
    {
        amount = input.text;
    }
}
