using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum TypeSwap{send,get}
public class SwapSceneItem : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI nameCoinText;
    public Button button;
    public TypeSwap typeSwap;

    string nameCoin;

    void Start()
    {
        button.onClick.AddListener(OnClickChoose);
    }

    public void SetProperties(string _nameCoin)
    {
        nameCoin =_nameCoin;
        icon.sprite = ClientData.Instance.GetSpriteIcon(nameCoin).sprite;
        nameCoinText.text = nameCoin;
    }

    //Button
    void OnClickChoose()
    {

    }

}
