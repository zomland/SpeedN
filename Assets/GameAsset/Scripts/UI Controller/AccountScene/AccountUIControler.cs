using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class AccountUIControler : MonoBehaviour
{
    public GameObject MainCv, ProfileCv, MovingRecordCv, MovingRecordDetailCv
    , SettingCv, SettingNetworkCv, SettingLanguageCv, HelpAndSupportCv
    , FAQDetailsCv, SupportCv, PopUpLogoutCv;
    public GameObject[] networkTickSigns;
    public GameObject[] languageTickSigns;
    public RectTransform ViewportContentAnswerRect;
    public RectTransform ContentAnswerFAQRect;

    Dictionary<string, GameObject> CanvasDictionary = new Dictionary<string, GameObject>();
    Dictionary<string, GameObject[]> TickSignDictionary = new Dictionary<string, GameObject[]>();
    int optNetwork = 0;
    int optLanguage = 0;
    bool isMute = false;
    float maxYOfContentAnswerRect;
    
    string currentLanguage;
    string currentNetwork;
    string currnetState="Main";



    private void Start()
    {
        maxYOfContentAnswerRect=ViewportContentAnswerRect.rect.height
        -1400f;

        CanvasDictionary["Main"] = MainCv;
        CanvasDictionary["Profile"] = ProfileCv;
        CanvasDictionary["MovingRecord"] = MovingRecordCv;
        CanvasDictionary["MovingRecordDetail"] = MovingRecordDetailCv;
        CanvasDictionary["Setting"] = SettingCv;
        CanvasDictionary["SettingNetwork"] = SettingNetworkCv;
        CanvasDictionary["SettingLanguage"] = SettingLanguageCv;
        CanvasDictionary["HelpAndSupport"] = HelpAndSupportCv;
        CanvasDictionary["FAQDetails"] = FAQDetailsCv;
        CanvasDictionary["Support"] = SupportCv;
        CanvasDictionary["PopUpLogout"] = PopUpLogoutCv;

        TickSignDictionary["Network"] = networkTickSigns;
        TickSignDictionary["Language"] = languageTickSigns;
    }

    public void ActiveCanvas(string name)
    {
        Debug.Log("Go to " + name);
        currnetState=name;
        foreach (KeyValuePair<string, GameObject> element in CanvasDictionary)
        {
            if (name == "PopUpLogout" & element.Key == "Main")
            {
                continue;
            }
            else
            {
                element.Value.SetActive(false);
            }
        }
        CanvasDictionary[name].SetActive(true);
    }

    public void DeleteAccount()
    {
        Debug.LogWarning("Delete account");
    }

    public void LoadMovingRecordData()
    {
        Debug.Log("Load data record");
    }

    public void LoadPage()
    {
        Debug.Log("Load page");
    }

    public void DownloadRecord()
    {
        Debug.Log("Download Record");
    }
    public void ShareRecord()
    {
        Debug.Log("Share Record");
    }

    public void ChangeSoundSetting()
    {
        isMute = !isMute;
        if (isMute)
        {
            Debug.LogWarning("Sound is mute");
        }
        else
        {
            Debug.LogWarning("Sound is on");
        }
    }

    public void ChangeTickSign(string key, int opt)
    {
        GameObject[] TickSigns = TickSignDictionary[key];
        foreach (GameObject tickSign in TickSigns)
        {
            tickSign.SetActive(false);
        }
        TickSigns[opt].SetActive(true);
    }

    public void ChangeOptNetwork(int opt)
    {
        optNetwork = opt;
        Debug.Log("Change option network to " + opt.ToString());
        ChangeTickSign("Network", opt);
    }

    public void ChangeOptLanguage(int opt)
    {
        optLanguage = opt;
        Debug.Log("Change option language to " + opt.ToString());
        ChangeTickSign("Language", opt);
    }

    public void LoadFAQDetialData(int numQues)
    {
        Debug.Log("Load data FAQ " + numQues.ToString());
    }

    public void GotoCommunity(int opt)
    {
        switch (opt)
        {
            case 0:
                GotoDiscord();
                break;
            case 1:
                GotoEnglishTelegram();
                break;
            case 2:
                GotoVietnameseTelagram();
                break;
            case 3:
                GotoTwitter();
                break;
            case 4:
                GotoMedium();
                break;
            case 5:
                GotoReddit();
                break;
            default:
                Debug.LogError("Go to community error");
                break;
        }
    }

    void GotoDiscord()
    {
        Debug.Log("Go to Discord");
    }

    void GotoEnglishTelegram()
    {
        Debug.Log("Go to EnglishTelegram");
    }
    void GotoVietnameseTelagram()
    {
        Debug.Log("Go to VietnameseTelagram");
    }
    void GotoTwitter()
    {
        Debug.Log("Go to Twitter");
    }
    void GotoMedium()
    {
        Debug.Log("Go to Medium");
    }
    void GotoReddit()
    {
        Debug.Log("Go to Reddit");
    }
    void ControlScrollRect()
    {
        if(currnetState=="FAQDetails")
        {
            if (ContentAnswerFAQRect.localPosition.y<0)
            {
                ContentAnswerFAQRect.localPosition=new Vector3(0f,0f,0f);
            }
            if (ContentAnswerFAQRect.localPosition.y>maxYOfContentAnswerRect)
            {
                ContentAnswerFAQRect.localPosition=new Vector3(0f,maxYOfContentAnswerRect,0f);
            }
        }
    }

    private void Update() {
        ControlScrollRect();
    }
}
