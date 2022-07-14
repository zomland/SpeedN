using Base.Helper;
using DG.Tweening;
using UnityEngine;

public class LoadingProgress : BaseMono, UIBase
{
    [SerializeField] private RectTransform loadingIcon;
    [SerializeField] private GameObject panel;
    
    public void Show()
    {
        panel.SetActive(true);
        
        PlayAnimation();
    }

    public void Hide()
    {
        StopAnimation();
        
        panel.SetActive(false);
    }

    private void PlayAnimation()
    {
        loadingIcon.DORotate(Vector3.forward * 180f, .5f).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
    }

    private void StopAnimation()
    {
        loadingIcon.DOKill();
    }
}
