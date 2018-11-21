using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PopupWindowTemplate : MonoBehaviour {
    public CanvasGroup canvasGroup;
    public RectTransform windowRectTransform;

    protected Vector2 startPotion = new Vector2(0, -80);
    protected Vector2 endPotion = new Vector2(0, -50);
    protected float animationTime = 0.4f;

    public  bool isOpen { get; set; }
    private bool canAnimetion = true;

    public void InEffect()
    {
        if (canAnimetion == false) return;
        canAnimetion = false;

        InEffectStartEvent();

        windowRectTransform.anchoredPosition = startPotion;
        canvasGroup.alpha = 0;
        Sequence sequence = DOTween.Sequence();
        sequence.Join(windowRectTransform.DOAnchorPos(endPotion, animationTime).SetEase(Ease.OutCubic))
                .Join(DOTween.To(() => canvasGroup.alpha, num => canvasGroup.alpha = num, 1, animationTime))
                .OnComplete(
                () =>{
                    this.isOpen = true;
                    this.canAnimetion = true;
                    canvasGroup.interactable = true;
                    canvasGroup.blocksRaycasts = true;
                    InEffectEndEvent();
                });

        sequence.Play();
    }

    public void OutEffect()
    {
        if (canAnimetion == false) return;
        canAnimetion = false;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        OutEffectStartEvent();

        Sequence sequence = DOTween.Sequence();
        sequence.Join(windowRectTransform.DOAnchorPos(startPotion, animationTime).SetEase(Ease.OutCubic))
                .Join(DOTween.To(() => canvasGroup.alpha, num => canvasGroup.alpha = num, 0, animationTime))
                .OnComplete(
                () => {
                    this.isOpen = false;
                    this.canAnimetion = true;
                    OutEffectEndEvent();
                });
        sequence.Play();

    }

    protected virtual void InEffectStartEvent()
    {

    }

    protected virtual void InEffectEndEvent()
    {

    }

    protected virtual void OutEffectStartEvent()
    {

    }


    protected virtual void OutEffectEndEvent()
    {

    }

}
