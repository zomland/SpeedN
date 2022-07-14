using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.SimpleLocalization;
using UnityEngine.UI;
public class Multi : MonoBehaviour
{
    private void Awake()
    {
        LocalizationManager.Read();

        switch (Application.systemLanguage)
        {
            case SystemLanguage.English:
                LocalizationManager.Language ="English";
                break;
            case SystemLanguage.Vietnamese:
                LocalizationManager.Language= "VietNam";
                break ;
        }
    }

    public void Languge(string language)
    {
        LocalizationManager.Language = language;
    }
}
