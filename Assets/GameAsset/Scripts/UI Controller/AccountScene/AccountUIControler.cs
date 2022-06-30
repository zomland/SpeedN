using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Newtonsoft.Json;
using Base.Audio;
using Translation;

public class AccountUIControler : MonoBehaviour
{
    [Header("Canvas")]
    public GameObject MainCv;
    public GameObject ProfileCv;
    public GameObject MovingRecordCv;
    public GameObject MovingRecordDetailCv;
    public GameObject SettingCv;
    public GameObject SettingNetworkCv;
    public GameObject SettingLanguageCv;
    public GameObject SettingSoundCv;
    public GameObject HelpAndSupportCv;
    public GameObject FAQDetailsCv;
    public GameObject SupportCv;
    public GameObject PopUpLogoutCv;

    [Header("UI Element")]
    public RectTransform ViewportRectFAQ;
    public RectTransform ContentRectFAQ;
    public RectTransform ContentAnswerRect;
    public ScrollRect AnswerScrollRect;
    public MovingRecordOnAccountControler movingRecordOAControler;
    public GameObject ButtonsInfoRecord;
    public Text textTotalKm;
    public Text textTotalTime;
    public Text textTotalKmOnMain;

    [Header("TickSigns")]
    public GameObject[] networkTickSigns;
    public GameObject[] languageTickSigns;

    Dictionary<string, GameObject> CanvasDictionary = new Dictionary<string, GameObject>();
    Dictionary<string, GameObject[]> TickSignDictionary = new Dictionary<string, GameObject[]>();
    int optNetwork = 0;
    int optLanguage = 0;
    float maxHeightOfContentRect;
    const int secondsPerHour = 3600;
    const int secondPerMin = 60;

    string currentLanguage;
    string currentNetwork;
    string currentState = "Main";
    SettingSound settingSound;



    private void Start()
    {
        maxHeightOfContentRect = ContentAnswerRect.rect.height - ViewportRectFAQ.rect.height;
        //Canvas
        CanvasDictionary["Main"] = MainCv;
        CanvasDictionary["Profile"] = ProfileCv;
        CanvasDictionary["MovingRecord"] = MovingRecordCv;
        CanvasDictionary["MovingRecordDetail"] = MovingRecordDetailCv;
        CanvasDictionary["Setting"] = SettingCv;
        CanvasDictionary["SettingNetwork"] = SettingNetworkCv;
        CanvasDictionary["SettingLanguage"] = SettingLanguageCv;
        CanvasDictionary["SettingSound"] = SettingSoundCv;
        CanvasDictionary["HelpAndSupport"] = HelpAndSupportCv;
        CanvasDictionary["FAQDetails"] = FAQDetailsCv;
        CanvasDictionary["Support"] = SupportCv;
        CanvasDictionary["PopUpLogout"] = PopUpLogoutCv;
        //TIckSign
        TickSignDictionary["Network"] = networkTickSigns;
        TickSignDictionary["Language"] = languageTickSigns;
        //Setting Sound
        settingSound = SettingSoundCv.GetComponent<SettingSound>();

        ShowTotalOnMovingRecordState();
        ChangeOptLanguage((int)Translator.CurrentLanguage);
    }

    void ShowTotalOnMovingRecordState()
    {
        if (ClientData.Instance != null)
        {
            //km
            string totalKmString;
            if (ClientData.Instance.ClientUser.totalKm < 0.01)
            {
                totalKmString = (ClientData.Instance.ClientUser.totalKm * 1000).ToString("0.00");
                totalKmString += " m";
            }
            else
            {
                totalKmString = ClientData.Instance.ClientUser.totalKm.ToString("0.00");
                totalKmString += " km";
            }
            textTotalKmOnMain.text = totalKmString;
            textTotalKm.text = totalKmString;

            //time
            string totalTimeString;
            if (ClientData.Instance.ClientUser.totalTime / secondsPerHour < 1f)
            {
                totalTimeString = (ClientData.Instance.ClientUser.totalTime / secondPerMin)
                    .ToString("0.00");
                totalTimeString += " mins";
            }
            else
            {
                totalTimeString = ClientData.Instance.ClientUser.totalTime.ToString("0.00");
                totalTimeString += " hours";
            }
            textTotalTime.text = totalTimeString;
        }
    }

    public void ActiveCanvas(string name)
    {
        Debug.Log("Go to " + name);
        currentState = name;
        if (name == "MovingRecord") movingRecordOAControler.isOnMovingRecordState = true;
        else movingRecordOAControler.isOnMovingRecordState = false;
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
        if (name == "SettingSound") settingSound.onSettingSound = true;
        else settingSound.onSettingSound = false;
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
        Translator.CurrentLanguage = (Language)opt;
        ChangeTickSign("Language", opt);
    }

    public void LoadFAQDetailData(int numQues)
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
                GotoVietnameseTelegram();
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
    void GotoVietnameseTelegram()
    {
        Debug.Log("Go to VietnameseTelegram");
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
    void ControlScrollRectFAQ()
    {
        if (currentState == "FAQDetails")
        {
            if (ContentRectFAQ.localPosition.y <= 0f)
            {
                ContentRectFAQ.localPosition = new Vector3(0f, 0f, 0f);
                AnswerScrollRect.decelerationRate = 0f;
            }
            else
            {
                AnswerScrollRect.decelerationRate = 0.135f;
            }
            if (ContentRectFAQ.localPosition.y >= maxHeightOfContentRect)
            {
                ContentRectFAQ.localPosition = new Vector3(0f, maxHeightOfContentRect, 0f);
                AnswerScrollRect.decelerationRate = 0f;
            }
            else
            {
                if (ContentRectFAQ.localPosition.y > 0f)
                    AnswerScrollRect.decelerationRate = 0.135f;
            }
        }
    }




    private void Update()
    {
        ControlScrollRectFAQ();
    }
}
