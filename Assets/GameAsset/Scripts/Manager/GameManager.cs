using System;
using System.Collections;
using System.Collections.Generic;
using Base;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private LoadingProgress loadingProgress;

    public void ShowLoading()
    {
        loadingProgress.Show();
    }

    public void HideLoading()
    {
        loadingProgress.Hide();
    }
}
