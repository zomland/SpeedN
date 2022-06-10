using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioClipBase
{
    public string name;
    public string ID;
    public AudioClip clip;

    public AudioClipBase()
    {
        name = "";
        ID = "Null";
        clip = null;
    }
}

[System.Serializable]
public class musicAudioClip : AudioClipBase
{
    public musicAudioClip() { }
}
[System.Serializable]
public class soundAudioClip : AudioClipBase
{
    public soundAudioClip() { }
}
[System.Serializable]
public class UISoundAudioClip : AudioClipBase
{
    public UISoundAudioClip() { }
}
