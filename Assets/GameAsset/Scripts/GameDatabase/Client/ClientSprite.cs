using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class SpriteIcon : SpriteBase{
    public SpriteIcon(){}
}

[System.Serializable]
public class SpriteModelVehicle : SpriteBase{
    public SpriteModelVehicle(){}
}

[System.Serializable]
public class SpriteBase{
    public string spriteID;
    public Sprite sprite;

    public SpriteBase(){
        spriteID = String.Empty;
        sprite = null;
    }
}

