using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ImportSceneController : MonoBehaviour
{
    public GameObject inputField;
    public Button importButton;

    float amountImport;

    void Start()
    {
        importButton.onClick.AddListener(Import);
    }

    private void OnDestroy()
    {
        importButton.onClick.RemoveListener(Import);
    }

    private void Import()
    {
        string tmp = inputField.GetComponent<TMP_InputField>().text;
        if (tmp == "")
        {
            amountImport = 0;
        }
        else
        {
            amountImport = float.Parse(tmp);
        }
        Debug.Log(amountImport);
        PlayerPrefs.SetFloat("AmountImport",amountImport);
    }
}
