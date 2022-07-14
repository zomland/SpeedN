using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ImportSceneController : MonoBehaviour
{
    public TMP_InputField inputField;
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

    }
}
