using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextsNeedTranslate : MonoBehaviour
{
    public List<TextElement> TextElements;
}

[System.Serializable]
public class TextElement
{
    public string ID;
    public Text textComponent;
    public TextElement() { }
}

