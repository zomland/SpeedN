using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SwapUIController : MonoBehaviour
{
    [Header("Icon")]
    public Image iconSend;
    public Image iconGet;

    [Header("Name Coin")]
    public TextMeshProUGUI nameCoinSend;
    public TextMeshProUGUI nameCoinGet;

    [Header("CurrentAmount")]
    public TextMeshProUGUI amountSend;
    public TextMeshProUGUI amountGet;

    [Header("Input Field")]
    public TMP_InputField inputSend;
    public TMP_InputField inputGet;

    
}
