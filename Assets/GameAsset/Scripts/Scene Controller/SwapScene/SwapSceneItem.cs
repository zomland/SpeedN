using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SwapSceneItem : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI nameCoinText;
    public Button button;
    public GameObject choice;
    public int index;

    string nameCoin;
    SwapSceneData swapSceneData;

    void Start()
    {
        swapSceneData = FindObjectOfType<SwapSceneData>();
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
        FindObjectOfType<SwapSceneController>().ChooseCoin(index);
        choice.gameObject.SetActive(true);
        swapSceneData.ChangeSwapCoin(index,nameCoin);
    }

}
