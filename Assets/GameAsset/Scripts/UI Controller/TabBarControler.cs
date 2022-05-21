using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Base.MessageSystem;
using Global;

public class TabBarControler : MonoBehaviour
{
    public string currentScene;
    public void GotoHome()
    {
        Debug.Log("Go to Home");
        Messenger.RaiseMessage(Message.LoadScene, "HomeScene", currentScene);
    }

    public void GotoMarket()
    {
        Debug.Log("Go to Market");
        Messenger.RaiseMessage(Message.LoadScene, "MarketplaceScene", currentScene);
    }

    public void GotoChart()
    {
        Debug.Log("Go to Chart");
        Messenger.RaiseMessage(Message.LoadScene, "ChartScene", currentScene);
    }

    public void GotoMyItem()
    {
        Debug.Log("Go to MyItem");
        Messenger.RaiseMessage(Message.LoadScene, "MyItemScene", currentScene);
    }

    public void GotoAccount(string currentScene)
    {
        Debug.Log("Go to Account");
        Messenger.RaiseMessage(Message.LoadScene, "AccountScene", currentScene);
    }
}
