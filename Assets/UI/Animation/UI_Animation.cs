using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UI_Animation : MonoBehaviour
{
    // 팝업 애니메이션

    // 1. 팝업 열릴 때
    public void OpenPopupAnim(CanvasGroup popupCanvas, RectTransform popupTransform)
    {
        PopupOpen(popupCanvas, popupTransform);
    }

    // 2. 팝업 닫힐 때
    public void ClosePopupAnim(CanvasGroup popupCanvas, RectTransform popupTransform)
    {
        PopupOpen(popupCanvas, popupTransform);
    }
    
    private Sequence PopupOpen(CanvasGroup popupCanvas, RectTransform popupTransform)
    {
        return DOTween.Sequence()
            .OnStart(() => {
                popupCanvas.alpha = 0;
                popupTransform.localScale = new Vector2(0.95f, 0.95f);
            })
            .Join(popupCanvas.DOFade(1, 0.25f).SetEase(Ease.InOutSine))
            .Join(popupTransform.DOScale(new Vector2(1.05f, 1.05f), 0.25f).SetEase(Ease.InOutSine))
            .Append(popupTransform.DOScale(Vector2.one, 0.25f).SetEase(Ease.InOutSine));
    }

    private Sequence PopupClose(CanvasGroup popupCanvas, RectTransform popupTransform)
    {
        return DOTween.Sequence()
            .Join(popupCanvas.DOFade(0, 0.25f).SetEase(Ease.InOutSine))
            .Join(popupTransform.DOScale(new Vector2(0.95f, 0.95f), 0.25f).SetEase(Ease.InOutSine));
    }


    // 버튼 애니메이션

    // 1. 버튼 눌렀을 때
    public void BtnPressAnim(RectTransform btnRect)
    {
        btnRect.DOScale(new Vector2(0.9f, 0.9f), 0.175f).SetEase(Ease.InOutSine);
    }

    // 2. 버튼에서 손 떼었을 때
    public void BtnPressOutAnim(RectTransform btnRect)
    {
        btnRect.DOScale(Vector2.one, 0.175f).SetEase(Ease.InOutSine);
    }

}
