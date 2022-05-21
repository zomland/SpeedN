using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MyItemSceneUIController : MonoBehaviour
{
    [Header("Color Button")]
    public Color defaultColor;
    public Color onClickColor;

    [Header("Button")]
    public List<MyItemSceneMenuButton> listMenuButton;

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
}
