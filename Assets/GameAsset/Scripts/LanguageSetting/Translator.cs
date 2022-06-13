using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Base;
using Transaction;

namespace Translation
{
    public enum Language
    {
        English, Vietnamese, French, Chinese
    }

    public class Translator : Singleton<Translator>
    {
        public static Language CurrentLanguage = Language.English;


        public static void Translate(string sceneName)
        {
            TextsNeedTranslate textsNeedTranslate = FindObjectOfType<TextsNeedTranslate>();
            switch (sceneName)
            {
                case "HomeScene":
                    foreach (var child in textsNeedTranslate.TextElements)
                    {
                        try
                        {
                            child.textComponent.text = LanguageDict
                                .HomeLanguageDict[child.ID][(int)CurrentLanguage];
                        }
                        catch
                        {
                            child.textComponent.text = "ER: 404";
                            Debug.LogWarning("TextElement not found translation");
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        public static void Translate(Text textComponent, string ID)
        {
            try
            {
                textComponent.text = LanguageDict
                    .HomeLanguageDict[ID][(int)CurrentLanguage];
            }
            catch
            {
                textComponent.text = "ER: 404";
                Debug.LogWarning("TextElement not found translation");
            }
        }

    }
}
