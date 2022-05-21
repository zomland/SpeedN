using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class SpriteIcon : SpriteBase{
    public SpriteIcon(){}
}

[System.Serializable]
public class SpriteVehicle : SpriteBase{
    public SpriteVehicle(){}
}

[System.Serializable]
public class SpriteBase{
    public string name;
    public Sprite sprite;

    public SpriteBase(){
        name = String.Empty;
        sprite = null;
    }
}

