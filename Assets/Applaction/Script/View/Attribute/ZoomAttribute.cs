using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class ZoomAttribute : MonoBehaviour, IScrollHandler{
    [System.NonSerialized]
    public RectTransform myRect;

    [SerializeField, HeaderAttribute("0の場合は制限なし")]
    float max = 0;
    [SerializeField]
    float min = 0;

    void Start(){
        myRect = GetComponent<RectTransform>();
    }


    public void OnScroll(PointerEventData eventData){
        float scale = myRect.localScale.y + eventData.scrollDelta.y * 0.01f;
        if (max > 0) scale = (scale > max) ? max : scale;
        if (min > 0) scale = (scale < min) ? min : scale;
        myRect.localScale = new Vector3(scale, scale, 1.0f);
    }
}
