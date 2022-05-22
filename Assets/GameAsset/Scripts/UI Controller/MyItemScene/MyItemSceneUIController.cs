using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class MyItemSceneUIController : MonoBehaviour
{
    [Header("Color Button")]
    public Color defaultColor;
    public Color onClickColor;

    [Header("Button")]
    public List<MyItemSceneMenuButton> listMenuButton;

    [Header("PopUp")]
    public GameObject panelMain;
    public GameObject panelMyItemDetail;

    [Header("Coin")]
    public Image[] spriteTypeCoin;
    public TextMeshProUGUI [] coinAmountText;

    void Start()
    {
        DisplayCoinUI(); 
    }

    private void DisplayCoinUI()
    {
        var sortedList = ClientData.Instance.ClientUser.clientCoins.OrderByDescending(coin=>coin.amount).ToList();
        for(int i = 0;i< 3; i++)
        {
            
            coinAmountText[i].text = sortedList[i].amount.ToString();
            spriteTypeCoin[i].sprite = ClientData.Instance.GetSpriteIcon(sortedList[i].nameCoin).sprite;
        }
    }

    public void OnClickMenuButton(MyItemSceneMenuButton tmp)
    {
        foreach(var child in listMenuButton)
        {
            if(child == tmp) 
            {
                child.GetComponent<Image>().color = onClickColor;
                child.GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
            }
            else{
                child.GetComponent<Image>().color = defaultColor;
                child.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
            }
        }
    }

    public void OnClickVehicleItem(){
        panelMain.gameObject.SetActive(false);
        panelMyItemDetail.gameObject.SetActive(true);
    }
}
