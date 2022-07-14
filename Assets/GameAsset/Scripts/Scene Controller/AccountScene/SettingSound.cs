using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Base.Audio;

public class SettingSound : MonoBehaviour
{
    public Slider GeneralVolume;
    public Slider MusicVolume;
    public Slider SoundVolume;
    public Slider UISoundVolume;
    public Button buttonMute;
    public Text textStateMute;
    bool isMute;
    [HideInInspector] public bool onSettingSound;

    void Start()
    {
        GeneralVolume.value = SoundManager.GlobalVolume;
        MusicVolume.value = SoundManager.GlobalMusicVolume;
        SoundVolume.value = SoundManager.GlobalSoundsVolume;
        UISoundVolume.value = SoundManager.GlobalUISoundsVolume;
        isMute = SoundManager.GlobalMute;
        ChangeButtonGUI();
    }

    public void ChangeStateMute()
    {
        isMute = !isMute;
        ChangeButtonGUI();
        ChangeStateMuteBehavior();
    }

    void ChangeButtonGUI()
    {
        if (!isMute)
        {
            buttonMute.image.color = new Color(0, 255, 255, 255);
            textStateMute.text = "ON";
        }
        else
        {
            buttonMute.image.color = new Color(255, 194, 0, 255);
            textStateMute.text = "OFF";
        }
    }

    void ChangeStateMuteBehavior()
    {

        if (!isMute)
        {
            SoundManager.GlobalMute = false;
            SoundManager.PlayMusic(ClientData.Instance.GetAudioClip(Audio.AudioType.Music, "music001"));
        }
        else
        {
            SoundManager.GlobalMute = true;
            SoundManager.StopAll();
        }
    }

    void FixedUpdate()
    {
        if (onSettingSound)
        {
            SoundManager.SetVolumeGlobal(GeneralVolume.value);
            SoundManager.SetVolumeMusic(MusicVolume.value);
            SoundManager.SetVolumeSound(SoundVolume.value);
            SoundManager.SetVolumeUISound(UISoundVolume.value);

        }
    }
}
